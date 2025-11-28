---
description: Workflow for conducting performance reviews and tracking benchmark results
author: team
version: 1.0
globs: ["**/*"]
tags: ["performance-review", "benchmarks", "workflow"]
---

# Performance Review Workflow

## Process Overview

After implementing changes that may affect performance, conduct a performance review to detect regressions and track improvements.

Performance reviews can be conducted:

-   **Manually**: By running benchmarks locally and generating review documents
-   **Automatically**: Via the nightly automated workflow that runs on the main branch

## Automated Nightly Performance Workflow

The repository includes an automated nightly performance workflow (`.github/workflows/nightly-performance.yml`) that:

1. **Runs at 2:00 AM UTC** on the main branch
2. **Executes the full benchmark suite** in Release configuration
3. **Compares results** against the current baseline
4. **Generates review documents** automatically
5. **Creates GitHub issues** if regressions are detected

### Files Created by Nightly Workflow

The automated workflow creates/updates the following files in the `performance_reviews/ci/` folder:

#### Performance Review Reports

-   **Location**: `performance_reviews/ci/nightly/`
-   **Naming**: `performance-review-YYYY-MM-DD.md`
-   **Purpose**: Daily performance review with comparison tables and regression analysis
-   **Retention**: Kept indefinitely for historical trend tracking

#### Baseline Updates (No Regressions)

-   **Location**: `performance_reviews/ci/baselines/baseline-current.json`
-   **Purpose**: Current approved baseline for all benchmarks
-   **Update**: Automatically updated when no regressions are detected
-   **Commit**: Auto-committed with message `chore: update performance baseline - YYYY-MM-DD`

#### GitHub Issues (Regressions Detected)

-   **Labels**: `performance-regression`, `severity-{critical|major|minor}`
-   **Content**: Full performance review with regression details
-   **Purpose**: Track and manage performance regressions

## Local Performance Reviews

Local performance reviews are conducted manually by developers and should be stored in user-specific subfolders to avoid conflicts with CI-generated files.

### File Organization for Local Reviews

-   **Location**: `performance_reviews/user/{user_name}/`
-   **Purpose**: Isolate local performance reviews and baselines from CI-generated content
-   **Naming Convention**: Use descriptive subfolder names (e.g., `farooq_laptop`, `john_desktop`)
-   **Access Control**: Only the GitHub workflow should check-in changes to `performance_reviews/ci/`

### Files Created by Local Reviews

#### Performance Review Reports

-   **Location**: `performance_reviews/user/{user_name}/`
-   **Naming**: `performance-review-YYYYMMDD.md` (e.g., `performance-review-11082025.md`)
-   **Purpose**: Local performance review with comparison tables and regression analysis

#### Baseline Files

-   **Current Baseline**: `performance_reviews/user/{user_name}/baseline-current.json`
-   **New Baseline**: `performance_reviews/user/{user_name}/baseline-new.json`
-   **Purpose**: Track personal baseline performance for local comparisons

## When to Conduct Performance Reviews

Perform performance reviews:

-   After implementing new features
-   After refactoring code
-   After optimizing algorithms or data structures
-   Before releasing a new version
-   When code review findings have been addressed

## Performance Review Process

### 1. Run Benchmarks

Execute the benchmark suite to get current performance measurements:

```bash
dotnet run -c Release --project Teqniqly.Results.Benchmarks/Teqniqly.Results.Benchmarks.csproj
```

### 2. Run Comparison Script

Run the comparison script specifying your user folder and commit hash. For example:

```bash
python .github/scripts/compare-benchmarks.py --baseline performance_reviews/user/farooq_laptop/baseline-current.json --cpu-results BenchmarkDotNet.Artifacts/results/Teqniqly.Results.Benchmarks.ResultCpuBenchmarks-report-github.md --memory-results BenchmarkDotNet.Artifacts/results/Teqniqly.Results.Benchmarks.ResultMemoryBenchmarks-report-github.md --output performance_reviews/user/farooq_laptop/performance-review-11082025.md --new-baseline performance_reviews/user/farooq_laptop/baseline-new.json --commit f4b494eeade680b1ae621e03605a5f09966ffd4f
```

### 3. Document Structure

The performance review document should include:

#### Header

```markdown
# Performance Review Results

**Date**: YYYY-MM-DD HH:MM:SS
**Reviewer**: [Name or System]
**Baseline**: [Previous baseline date or version]
```

#### Summary Section

-   Total benchmarks analyzed
-   Number of regressions found
-   Number of improvements found
-   Overall assessment (Pass/Fail)

#### CPU Benchmarks Comparison

For each CPU benchmark:

-   Benchmark name
-   Baseline mean time
-   Current mean time
-   Difference (absolute and percentage)
-   Status: ✅ Improved, ⚠️ Regression, ➡️ No significant change
-   Threshold for significance: ±5%

#### Memory Benchmarks Comparison

For each memory benchmark:

-   Benchmark name
-   Baseline allocated memory
-   Current allocated memory
-   Baseline Gen0/Gen1/Gen2 collections
-   Current Gen0/Gen1/Gen2 collections
-   Difference (absolute and percentage)
-   Status: ✅ Improved, ⚠️ Regression, ➡️ No significant change
-   Threshold for significance: ±5%

#### Regression Details

For any benchmarks showing regression:

-   Detailed analysis of the regression
-   Possible causes (if identifiable)
-   Severity: Critical, Major, Minor
-   Recommendation: Fix before merge, Monitor, Acceptable tradeoff

#### Action Items

-   List of required actions based on findings
-   Priority of each action
-   Suggested next steps

## Regression Criteria

### CPU Performance

-   **Critical Regression**: >20% slower
-   **Major Regression**: 10-20% slower
-   **Minor Regression**: 5-10% slower
-   **No Significant Change**: <5% difference
-   **Improvement**: Any measurable speedup

### Memory Performance

-   **Critical Regression**: >20% more allocations or Gen2 collections increase
-   **Major Regression**: 10-20% more allocations or Gen1 collections increase
-   **Minor Regression**: 5-10% more allocations
-   **No Significant Change**: <5% difference
-   **Improvement**: Any measurable reduction in allocations or GC pressure

## Decision Matrix

### No Regressions Found

1. Document the successful review in the performance review file
2. Update the README.md with new baseline numbers
3. Update benchmark documentation if needed
4. Commit the performance review file for historical tracking
5. Proceed with merge/release

### Regressions Found

1. Document all regressions in the performance review file
2. **DO NOT** update README baseline numbers
3. Notify the user of regressions with severity levels
4. Wait for user decision:
    - Fix the regression
    - Accept the regression with justification
    - Investigate further before deciding
5. Only update baselines after regression is resolved or accepted

## Baseline Update Process

When updating baselines (after confirming no regressions or accepting them):

### README.md Updates

Update the Performance Benchmarks section with:

-   New CPU benchmark table
-   New memory benchmark table
-   Updated key takeaways if metrics changed significantly
-   Timestamp of when baselines were established

### Benchmark Documentation Updates

Update the benchmark project's README.md:

-   Replace baseline results tables
-   Update any performance highlights
-   Add notes about significant changes from previous baseline

## Historical Tracking

### Performance Review Files

-   Keep all performance review files in the `performance_reviews` folder
-   Local reviews: Store in `performance_reviews/user/{user_name}/` subfolders
-   CI reviews: Store in `performance_reviews/ci/` folder (only CI should check-in changes here)
-   Never delete old performance reviews
-   Use for trend analysis and regression tracking
-   Reference in commit messages when relevant

### Baseline References

In each performance review, clearly document:

-   Which baseline is being compared against
-   Date and commit hash of baseline
-   Reason for comparison (e.g., post-feature, pre-release)

## Performance Review Template

```markdown
# Performance Review Results

**Date**: [Date and time]
**Baseline**: [Baseline identifier]
**Commit**: [Current commit hash]

## Summary

-   **Total Benchmarks**: [Number]
-   **Regressions**: [Number]
-   **Improvements**: [Number]
-   **Status**: ✅ PASS / ⚠️ REGRESSIONS FOUND

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status   |
| --------- | -------- | ------- | ------ | -------- |
| Method1   | X.XX ns  | Y.YY ns | +Z%    | [Status] |

## Memory Benchmarks

| Benchmark | Baseline Alloc | Current Alloc | Change | Status   |
| --------- | -------------- | ------------- | ------ | -------- |
| Method1   | XX KB          | YY KB         | +Z%    | [Status] |

## Regressions

### [Benchmark Name] - [Severity]

-   **Baseline**: [Value]
-   **Current**: [Value]
-   **Change**: [Percentage]
-   **Analysis**: [Why this regression occurred]
-   **Recommendation**: [Fix/Monitor/Accept]

## Action Items

1. [ ] [Action item 1]
2. [ ] [Action item 2]

## Conclusion

[Overall assessment and recommended next steps]
```

## Best Practices

1. **Run Multiple Times**: Execute benchmarks multiple times to ensure consistency
2. **Clean Environment**: Close unnecessary applications, run on quiet system
3. **Same Configuration**: Always use Release configuration
4. **Document Context**: Note any system changes, .NET version updates, etc.
5. **Version Control**: Commit performance reviews with code changes
6. **Trend Analysis**: Review historical performance reviews for patterns
7. **Automate Where Possible**: Consider CI/CD integration for automatic reviews

## Integration with Code Review

Performance reviews complement code reviews:

1. **Code Review First**: Complete code review and address findings
2. **Implement Changes**: Apply code review feedback
3. **Run Tests**: Ensure all tests pass
4. **Performance Review**: Run benchmarks and compare
5. **Final Assessment**: Both code quality and performance acceptable
6. **Merge**: Proceed only when both reviews pass

## Example Scenarios

### Scenario 1: Feature Addition

```markdown
Added caching feature to Result creation

-   CPU: 15% faster (✅ Improvement)
-   Memory: 10% more allocations (⚠️ Minor regression due to cache)
-   Decision: Accept - tradeoff justified by speed improvement
```

### Scenario 2: Refactoring

```markdown
Refactored error handling logic

-   CPU: 3% slower (➡️ No significant change)
-   Memory: Same allocations (➡️ No change)
-   Decision: Accept - within acceptable variance
```

### Scenario 3: Algorithm Change

```markdown
Changed sorting algorithm

-   CPU: 25% slower (⚠️ Critical regression)
-   Memory: 5% less allocations (✅ Minor improvement)
-   Decision: Investigate - speed regression unacceptable
```

## Continuous Improvement

Use performance reviews to:

-   Identify optimization opportunities
-   Track long-term performance trends
-   Validate optimization efforts
-   Document performance characteristics
-   Build performance awareness in the team
