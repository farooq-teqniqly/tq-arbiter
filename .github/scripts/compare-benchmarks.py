#!/usr/bin/env python3
"""
Compare BenchmarkDotNet results against baseline and detect performance regressions.
"""

import argparse
import csv
import json
import re
import sys
from datetime import datetime
from pathlib import Path

# Benchmark type constants
BENCHMARK_TYPE_CPU = "cpu"
BENCHMARK_TYPE_MEMORY = "memory"


class BenchmarkResult:
    """Represents a single benchmark result."""

    def __init__(
        self,
        name: str,
        mean_ns: float,
        error_ns: float = 0,
        stddev_ns: float = 0,
        allocated_bytes: int = 0,
        gen0: float = 0,
        gen1: float = 0,
        benchmark_type: str = BENCHMARK_TYPE_CPU,
    ):
        self.name = name
        self.mean_ns = mean_ns
        self.error_ns = error_ns
        self.stddev_ns = stddev_ns
        self.allocated_bytes = allocated_bytes
        self.gen0 = gen0
        self.gen1 = gen1
        self.benchmark_type = benchmark_type


class RegressionDetector:
    """Detects performance regressions by comparing results."""

    THRESHOLD_MINOR = 0.05  # 5%
    THRESHOLD_MAJOR = 0.10  # 10%
    THRESHOLD_CRITICAL = 0.20  # 20%

    @staticmethod
    def compare_cpu(
        baseline: BenchmarkResult, current: BenchmarkResult
    ) -> tuple[str, float, str]:
        """Compare CPU benchmark results. Returns (status, change_pct, severity)."""
        if baseline.mean_ns == 0:
            return "➡️", 0.0, "NONE"

        change_pct = ((current.mean_ns - baseline.mean_ns) / baseline.mean_ns) * 100

        if abs(change_pct) < RegressionDetector.THRESHOLD_MINOR * 100:
            return "➡️", change_pct, "NONE"
        elif change_pct < 0:
            return "✅", change_pct, "IMPROVEMENT"
        elif change_pct > RegressionDetector.THRESHOLD_CRITICAL * 100:
            return "⚠️", change_pct, "CRITICAL"
        elif change_pct > RegressionDetector.THRESHOLD_MAJOR * 100:
            return "⚠️", change_pct, "MAJOR"
        else:
            return "⚠️", change_pct, "MINOR"

    @staticmethod
    def compare_memory(
        baseline: BenchmarkResult, current: BenchmarkResult
    ) -> tuple[str, float, str]:
        """Compare memory benchmark results. Returns (status, change_pct, severity)."""
        if baseline.allocated_bytes == 0:
            return "➡️", 0.0, "NONE"

        change_pct = (
            (current.allocated_bytes - baseline.allocated_bytes)
            / baseline.allocated_bytes
        ) * 100

        if abs(change_pct) < RegressionDetector.THRESHOLD_MINOR * 100:
            return "➡️", change_pct, "NONE"
        elif change_pct < 0:
            return "✅", change_pct, "IMPROVEMENT"
        elif change_pct > RegressionDetector.THRESHOLD_CRITICAL * 100:
            return "⚠️", change_pct, "CRITICAL"
        elif change_pct > RegressionDetector.THRESHOLD_MAJOR * 100:
            return "⚠️", change_pct, "MAJOR"
        else:
            return "⚠️", change_pct, "MINOR"


def _clean_numeric_value(value_str: str, default: float = 0.0, is_int: bool = False) -> float | int:
    """Clean and convert numeric CSV values with proper unit normalization.

    Handles time units (s, ms, us/µs, ns) -> nanoseconds
    Handles size units (B, KB, MB, GB) -> bytes
    """
    if not value_str or value_str.strip() == "-" or value_str.strip() == "":
        return default

    # Trim whitespace and remove quotes
    cleaned = value_str.strip().replace('"', "")

    # Use regex to extract numeric value and unit (ReDoS-safe)
    match = re.match(r'^([0-9,]++(?:\.\d++)?)\s*(.*)$', cleaned)
    if not match:
        return default

    num_str, unit = match.groups()
    num_str = num_str.replace(",", "")  # Remove commas

    try:
        value = float(num_str)
    except ValueError:
        return default

    # Normalize units
    unit = unit.strip().lower()

    # Time unit conversions to nanoseconds
    if unit in ("s", "sec"):
        value *= 1_000_000_000  # seconds to nanoseconds
    elif unit in ("ms", "msec"):
        value *= 1_000_000  # milliseconds to nanoseconds
    elif unit in ("us", "µs", "μs"):
        value *= 1_000  # microseconds to nanoseconds
    elif unit == "ns":
        pass  # already in nanoseconds
    # Size unit conversions to bytes
    elif unit == "kb":
        value *= 1_024  # kilobytes to bytes
    elif unit == "mb":
        value *= 1_024 * 1_024  # megabytes to bytes
    elif unit == "gb":
        value *= 1_024 * 1_024 * 1_024  # gigabytes to bytes
    elif unit in ("b", ""):
        pass  # already in bytes or no unit
    else:
        # Unknown unit, return default
        return default

    return int(value) if is_int else value


def parse_csv_results(csv_path: str, benchmark_type: str = "cpu") -> list[BenchmarkResult]:
    """Parse BenchmarkDotNet CSV results into benchmark results."""
    results = []
    try:
        with open(csv_path, "r", encoding="utf-8") as f:
            reader = csv.DictReader(f)
            for row in reader:
                results.append(BenchmarkResult(
                    name=row["Method"],
                    mean_ns=_clean_numeric_value(row.get("Mean", "0")),
                    error_ns=_clean_numeric_value(row.get("Error", "0")),
                    stddev_ns=_clean_numeric_value(row.get("StdDev", "0")),
                    allocated_bytes=_clean_numeric_value(row.get("Allocated", "0"), is_int=True),
                    gen0=_clean_numeric_value(row.get("Gen0", "0")),
                    gen1=_clean_numeric_value(row.get("Gen1", "0")),
                    benchmark_type=benchmark_type,
                ))
    except (FileNotFoundError, KeyError, ValueError) as e:
        print(f"Error parsing CSV {csv_path}: {e}", file=sys.stderr)
        return []

    return results


def load_baseline(path: str) -> tuple[dict[str, BenchmarkResult], str]:
    """Load baseline from JSON file. Returns (results, baseline_date)."""
    try:
        with open(path, "r", encoding="utf-8") as f:
            data = json.load(f)
    except FileNotFoundError:
        # No baseline exists - this is the first run
        print(
            f"No baseline found at {path}. This will be the initial baseline.",
            file=sys.stderr,
        )
        return {}, "Initial Run"

    results = {}

    for bench in data.get("cpu_benchmarks", []):
        results[bench["name"]] = BenchmarkResult(
            name=bench["name"],
            mean_ns=bench["mean_ns"],
            error_ns=bench.get("error_ns", 0),
            stddev_ns=bench.get("stddev_ns", 0),
            allocated_bytes=bench.get("allocated_bytes", 0),
            benchmark_type=BENCHMARK_TYPE_CPU,
        )

    for bench in data.get("memory_benchmarks", []):
        results[bench["name"]] = BenchmarkResult(
            name=bench["name"],
            mean_ns=bench["mean_ns"],
            allocated_bytes=bench.get("allocated_bytes", 0),
            gen0=bench.get("gen0", 0),
            gen1=bench.get("gen1", 0),
            benchmark_type=BENCHMARK_TYPE_MEMORY,
        )

    baseline_date = data.get("date", "unknown")
    return results, baseline_date


def save_baseline(results: dict[str, BenchmarkResult], path: str, commit: str = ""):
    """Save results as new baseline JSON."""
    cpu_benchmarks = []
    memory_benchmarks = []

    for name, result in results.items():
        bench_data = {
            "name": result.name,
            "mean_ns": result.mean_ns,
            "error_ns": result.error_ns,
            "stddev_ns": result.stddev_ns,
            "allocated_bytes": result.allocated_bytes,
        }

        # Distinguish CPU vs Memory benchmarks by benchmark_type
        if result.benchmark_type == BENCHMARK_TYPE_MEMORY:
            bench_data["gen0"] = result.gen0
            bench_data["gen1"] = result.gen1
            memory_benchmarks.append(bench_data)
        else:
            cpu_benchmarks.append(bench_data)

    baseline = {
        "date": datetime.now().isoformat(),
        "commit": commit,
        "runtime": ".NET 9.0",
        "cpu_benchmarks": cpu_benchmarks,
        "memory_benchmarks": memory_benchmarks,
    }

    Path(path).parent.mkdir(parents=True, exist_ok=True)
    with open(path, "w", encoding="utf-8") as f:
        json.dump(baseline, f, indent=2)


def _is_memory_benchmark(result: BenchmarkResult) -> bool:
    """Determine if a benchmark is a memory benchmark."""
    return result.benchmark_type == BENCHMARK_TYPE_MEMORY


def _compare_benchmark(
    baseline_result: BenchmarkResult,
    current_result: BenchmarkResult,
) -> tuple[str, float, str, bool]:
    """Compare a single benchmark. Returns (status, change_pct, severity, is_memory)."""
    is_memory = _is_memory_benchmark(current_result)

    if is_memory:
        status, change_pct, severity = RegressionDetector.compare_memory(
            baseline_result, current_result
        )
    else:
        status, change_pct, severity = RegressionDetector.compare_cpu(
            baseline_result, current_result
        )

    return status, change_pct, severity, is_memory


def _build_summary_section(
    baseline_date: str,
    commit: str,
    total: int,
    regressions_count: int,
    improvements_count: int,
    max_severity: str,
) -> str:
    """Build the summary section of the review."""
    status = (
        "✅ PASS" if regressions_count == 0 else f"⚠️ REGRESSIONS FOUND ({max_severity})"
    )

    return f"""# Performance Review Results

**Date**: {datetime.now().strftime('%Y-%m-%d %H:%M:%S UTC')}
**Baseline**: {baseline_date}
**Commit**: {commit}

## Summary

- **Total Benchmarks**: {total}
- **Regressions**: {regressions_count}
- **Improvements**: {improvements_count}
- **Status**: {status}

"""


def _build_cpu_benchmarks_table(cpu_comparisons: list) -> str:
    """Build the CPU benchmarks comparison table."""
    md = """## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
"""

    for name, baseline_r, current_r, status, change, severity in cpu_comparisons:
        sign = "+" if change > 0 else ""
        severity_text = severity if severity not in ["NONE", "IMPROVEMENT"] else ""
        md += f"| {name} | {baseline_r.mean_ns:.3f} ns | {current_r.mean_ns:.3f} ns | {sign}{change:.1f}% | {status} {severity_text} |\n"

    return md


def _build_memory_benchmarks_table(memory_comparisons: list) -> str:
    """Build the memory benchmarks comparison table."""
    md = """\n## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
"""

    for name, baseline_r, current_r, status, change, severity in memory_comparisons:
        sign = "+" if change > 0 else ""
        gen_info = f"{current_r.gen0:.1f}/{current_r.gen1:.1f}"
        severity_text = severity if severity not in ["NONE", "IMPROVEMENT"] else ""
        md += f"| {name} | {baseline_r.allocated_bytes:,} B | {current_r.allocated_bytes:,} B | {sign}{change:.1f}% | {gen_info} | {status} {severity_text} |\n"

    return md


def _get_recommendation(severity: str) -> str:
    """Get recommendation based on regression severity."""
    if severity == "CRITICAL":
        return "Fix before merge"
    elif severity == "MINOR":
        return "Monitor"
    else:
        return "Investigate"


def _build_regressions_section(regressions: list) -> str:
    """Build the regressions detail section."""
    if not regressions:
        return ""

    md = "\n## Regressions\n\n"

    for name, baseline_r, current_r, change, severity in regressions:
        recommendation = _get_recommendation(severity)

        md += f"""### {name} - {severity}

- **Baseline**: {baseline_r.mean_ns:.3f} ns ({baseline_r.allocated_bytes:,} B allocated)
- **Current**: {current_r.mean_ns:.3f} ns ({current_r.allocated_bytes:,} B allocated)
- **Change**: +{change:.1f}%
- **Recommendation**: {recommendation}

"""

    return md


def _build_action_items_section(has_regressions: bool) -> str:
    """Build the action items section."""
    md = "\n## Action Items\n\n"

    if has_regressions:
        md += "- [ ] Review regression details above\n"
        md += "- [ ] Investigate root cause of performance degradation\n"
        md += "- [ ] Fix regression or document justification\n"
    else:
        md += "- [x] No regressions detected\n"
        md += "- [x] Baseline will be automatically updated\n"

    return md


def _build_conclusion_section(regressions_count: int, max_severity: str) -> str:
    """Build the conclusion section."""
    md = "\n## Conclusion\n\n"

    if regressions_count > 0:
        md += f"⚠️ **{regressions_count} regression(s) detected with {max_severity} severity.** "
        md += "Please review and address before baseline is updated.\n"
    else:
        md += "✅ **All benchmarks passed.** Performance is within acceptable range of baseline.\n"

    return md


def _generate_initial_baseline_review(
    current: dict[str, BenchmarkResult], baseline_date: str, commit: str
) -> str:
    """Generate review markdown for initial baseline establishment."""
    md = f"""# Performance Review Results - Initial Baseline

**Date**: {datetime.now().strftime('%Y-%m-%d %H:%M:%S UTC')}
**Baseline**: {baseline_date}
**Commit**: {commit}

## Summary

This is the **initial benchmark run**. No baseline exists for comparison.

- **Total Benchmarks**: {len(current)}
- **Status**: ✅ INITIAL BASELINE ESTABLISHED

## Benchmarks Recorded

The following benchmarks will serve as the baseline for future comparisons:

"""
    cpu_benchmarks = [
        r for name, r in current.items() if not _is_memory_benchmark(r)
    ]
    memory_benchmarks = [
        r for name, r in current.items() if _is_memory_benchmark(r)
    ]

    if cpu_benchmarks:
        md += "\n### CPU Benchmarks\n\n"
        for result in cpu_benchmarks:
            md += f"- **{result.name}**: {result.mean_ns:.3f} ns ({result.allocated_bytes} B)\n"

    if memory_benchmarks:
        md += "\n### Memory Benchmarks\n\n"
        for result in memory_benchmarks:
            md += f"- **{result.name}**: {result.mean_ns:.3f} ns ({result.allocated_bytes:,} B, Gen0/1: {result.gen0:.1f}/{result.gen1:.1f})\n"

    md += "\n## Next Steps\n\n"
    md += "- [x] Initial baseline established\n"
    md += "- [x] Future runs will compare against this baseline\n"
    md += "- [x] Performance regressions will be automatically detected\n"

    md += "\n## Conclusion\n\n"
    md += "✅ **Initial baseline successfully established.** Future benchmark runs will compare against these values.\n"

    return md


def _compare_all_benchmarks(
    baseline: dict[str, BenchmarkResult], current: dict[str, BenchmarkResult]
) -> tuple[list, list, list, list, str]:
    """Compare all benchmarks against baseline. Returns (cpu_comparisons, memory_comparisons, regressions, improvements, max_severity)."""
    cpu_comparisons = []
    memory_comparisons = []
    regressions = []
    improvements = []
    max_severity = "NONE"

    severity_rank = {"NONE": 0, "IMPROVEMENT": 0, "MINOR": 1, "MAJOR": 2, "CRITICAL": 3}

    for name, current_result in current.items():
        if name not in baseline:
            continue

        baseline_result = baseline[name]
        status, change_pct, severity, is_memory = _compare_benchmark(
            baseline_result, current_result
        )

        comparison = (
            name,
            baseline_result,
            current_result,
            status,
            change_pct,
            severity,
        )

        if is_memory:
            memory_comparisons.append(comparison)
        else:
            cpu_comparisons.append(comparison)

        if severity in ["MINOR", "MAJOR", "CRITICAL"]:
            regressions.append(
                (name, baseline_result, current_result, change_pct, severity)
            )
            if severity_rank[severity] > severity_rank[max_severity]:
                max_severity = severity
        elif severity == "IMPROVEMENT":
            improvements.append((name, baseline_result, current_result, change_pct))

    return cpu_comparisons, memory_comparisons, regressions, improvements, max_severity


def generate_review(
    baseline: dict[str, BenchmarkResult],
    current: dict[str, BenchmarkResult],
    baseline_date: str,
    commit: str,
) -> tuple[str, bool, str]:
    """Generate performance review markdown. Returns (content, has_regression, severity)."""

    # Handle initial baseline scenario
    if not baseline:
        md = _generate_initial_baseline_review(current, baseline_date, commit)
        return md, False, "NONE"

    # Compare benchmarks
    cpu_comparisons, memory_comparisons, regressions, improvements, max_severity = (
        _compare_all_benchmarks(baseline, current)
    )

    # Build markdown document
    md = _build_summary_section(
        baseline_date,
        commit,
        len(current),
        len(regressions),
        len(improvements),
        max_severity,
    )
    md += _build_cpu_benchmarks_table(cpu_comparisons)
    md += _build_memory_benchmarks_table(memory_comparisons)
    md += _build_regressions_section(regressions)
    md += _build_action_items_section(len(regressions) > 0)
    md += _build_conclusion_section(len(regressions), max_severity)

    return md, len(regressions) > 0, max_severity


def main():
    parser = argparse.ArgumentParser(
        description="Compare benchmark results against baseline"
    )
    parser.add_argument("--baseline", required=True, help="Path to baseline JSON file")
    parser.add_argument(
        "--cpu-results", required=True, help="Path to CPU benchmark CSV results"
    )
    parser.add_argument(
        "--memory-results",
        required=True,
        help="Path to memory benchmark CSV results",
    )
    parser.add_argument(
        "--output", required=True, help="Output path for performance review"
    )
    parser.add_argument(
        "--new-baseline", required=True, help="Output path for new baseline JSON"
    )
    parser.add_argument("--commit", default="", help="Git commit SHA")

    args = parser.parse_args()

    # Load baseline (returns tuple of results and date)
    baseline_results, baseline_date = load_baseline(args.baseline)

    # Parse current results
    current_results = {}

    # Parse CPU benchmark results
    for result in parse_csv_results(args.cpu_results, BENCHMARK_TYPE_CPU):
        current_results[result.name] = result

    # Parse memory benchmark results
    for result in parse_csv_results(args.memory_results, BENCHMARK_TYPE_MEMORY):
        current_results[result.name] = result

    # Generate review
    review_md, has_regression, severity = generate_review(
        baseline_results, current_results, baseline_date, args.commit
    )

    # Write review
    Path(args.output).parent.mkdir(parents=True, exist_ok=True)
    with open(args.output, "w", encoding="utf-8") as f:
        f.write(review_md)

    # Save new baseline (always save, workflow decides whether to use it)
    save_baseline(current_results, args.new_baseline, args.commit)

    # Create marker files for workflow
    if has_regression:
        with open(".regression-detected", "w", encoding="utf-8") as f:
            f.write("true")
        with open(".regression-severity", "w", encoding="utf-8") as f:
            f.write(severity)

    print(f"Performance review generated: {args.output}")
    print(f"Status: {'⚠️ REGRESSIONS FOUND' if has_regression else '✅ PASS'}")
    if has_regression:
        print(f"Severity: {severity}")

    return 1 if has_regression else 0


if __name__ == "__main__":
    sys.exit(main())
