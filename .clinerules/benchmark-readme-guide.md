---
description: Guide for creating README files for BenchmarkDotNet benchmark projects
author: team
version: 1.0
globs: ["**/Benchmarks/**/README.md", "**/*Benchmarks/README.md"]
tags: ["documentation", "readme", "benchmarks", "benchmarkdotnet"]
---

# Benchmark README Creation Guide

This guide provides a standardized template for creating README files for BenchmarkDotNet benchmark projects. It ensures consistency across benchmark projects and provides all necessary information for users to understand and run the benchmarks.

## Overview

Benchmark project READMEs should help developers understand what performance aspects are being measured, how to run the benchmarks, and how to interpret the results. They serve as both documentation and operational guides.

## README Structure Template

### 1. Project Header

**Purpose**: Clearly identify the benchmark project and its target library

**Components**:
- **Project Title**: Use format `{LibraryName}.Benchmarks`
- **Brief Description**: One sentence explaining what is being benchmarked

**Example**:
```markdown
# {LibraryName}.Benchmarks

This project contains CPU and memory benchmarks for the {LibraryName} library using BenchmarkDotNet.
```

### 2. Overview Section

**Purpose**: Explain the scope and types of benchmarks included

**Structure**:
- Brief introduction paragraph
- List of benchmark classes with descriptions
- Key performance areas covered

**Example**:
```markdown
## Overview

The benchmarks measure the performance characteristics of the {LibraryName} library across various scenarios:

### {LibraryName}CpuBenchmarks

CPU performance benchmarks focusing on execution speed:
- {Describe primary CPU benchmark scenarios}
- {List key CPU performance areas}

### {LibraryName}MemoryBenchmarks

Memory allocation benchmarks focusing on real-world usage patterns:
- {Describe primary memory benchmark scenarios}
- {List key memory performance areas}
```

### 3. Running the Benchmarks Section

**Purpose**: Provide clear instructions for executing benchmarks

**Subsections**:

#### Quick Run
```markdown
### Quick Run

To run all benchmarks with default settings:

```bash
dotnet run -c Release --project {LibraryName}.Benchmarks/{LibraryName}.Benchmarks.csproj
```
```

#### Specific Benchmark Classes
```markdown
### Run Specific Benchmark Class

To run only CPU benchmarks:

```bash
dotnet run -c Release --project {LibraryName}.Benchmarks/{LibraryName}.Benchmarks.csproj -- --filter *{LibraryName}CpuBenchmarks*
```

To run only memory benchmarks:

```bash
dotnet run -c Release --project {LibraryName}.Benchmarks/{LibraryName}.Benchmarks.csproj -- --filter *{LibraryName}MemoryBenchmarks*
```
```

#### Individual Methods
```markdown
### Run Specific Benchmark Method

To run a specific benchmark method:

```bash
dotnet run -c Release --project {LibraryName}.Benchmarks/{LibraryName}.Benchmarks.csproj -- --filter *{BenchmarkMethodName}*
```
```

### 4. Understanding the Results Section

**Purpose**: Explain how to interpret benchmark output

**Structure**:
- CPU benchmark metrics explanation
- Memory benchmark metrics explanation
- Common terminology

**Example**:
```markdown
## Understanding the Results

### CPU Benchmarks

CPU benchmark results include:
- **Mean**: Average execution time
- **Error**: Half of 99.9% confidence interval
- **StdDev**: Standard deviation of all measurements
- **Allocated**: Memory allocated per operation

### Memory Benchmarks

Memory benchmark results include:
- **Gen0**: Number of Gen 0 collections per 1000 operations
- **Gen1**: Number of Gen 1 collections per 1000 operations
- **Allocated**: Total memory allocated
```

### 5. Benchmark Configuration Section

**Purpose**: Document BenchmarkDotNet settings used

**Example**:
```markdown
## Benchmark Configuration

The benchmarks use the following BenchmarkDotNet configurations:
- **MemoryDiagnoser**: Enabled on all benchmarks to track memory allocations
- **SimpleJob** (Memory benchmarks only): 3 warmup iterations, 10 measurement iterations for consistent memory measurements
```

### 6. Output Section

**Purpose**: Explain where results are saved

**Example**:
```markdown
## Output

Results are saved in `BenchmarkDotNet.Artifacts` directory:
- **results/**: Contains detailed benchmark results in various formats (HTML, Markdown, CSV)
- **logs/**: Execution logs for debugging
- **bin/**: Compiled benchmark executables
```

### 7. Performance Considerations Section

**Purpose**: Provide guidance on interpreting results

**Example**:
```markdown
## Performance Considerations

When analyzing benchmark results, consider:
1. **CPU Performance**: Lower execution times indicate better performance
2. **Memory Allocations**: Fewer allocations reduce GC pressure
3. **GC Collections**: Fewer collections (especially Gen1+) indicate better memory efficiency
4. **Configuration Impact**: Different options have different performance characteristics
5. **Real-world Usage**: Consider how benchmarks reflect actual application usage patterns
```

### 8. Baseline Results Section

**Purpose**: Summarize current performance characteristics

**Structure**:
- CPU benchmark key takeaways
- Memory benchmark key takeaways
- Performance highlights and concerns

**Example**:
```markdown
## Baseline Results

### CPU Benchmarks

**Key Takeaways:**
- {Summarize key CPU performance findings}
- {Include specific timing data for critical operations}
- {Note any performance-sensitive areas}

### Memory Benchmarks

**Key Takeaways:**
- {Summarize key memory allocation findings}
- {Include allocation data for bulk operations}
- {Note any memory optimization opportunities}
```

### 9. Best Practices Section

**Purpose**: Guide proper benchmark execution

**Example**:
```markdown
## Best Practices for Running Benchmarks

1. **Close unnecessary applications** to reduce system noise
2. **Run in Release configuration** (never Debug) for accurate results
3. **Allow benchmarks to complete** without interruption
4. **Run multiple times** to verify consistency
5. **Compare relative performance** rather than absolute numbers across different machines
```

### 10. Contributing Section

**Purpose**: Guide adding new benchmarks

**Example**:
```markdown
## Contributing New Benchmarks

When adding new benchmarks:
1. Add methods to existing benchmark classes or create new ones
2. Use `[Benchmark]` attribute on benchmark methods
3. Include XML documentation explaining what is being measured
4. Use descriptive method names (underscores are allowed for readability)
5. Follow the existing naming patterns for consistency
```

### 11. Implementation Details Section

**Purpose**: Document technical implementation aspects

**Subsections**:
- Test data used
- Configuration options tested
- Iteration counts
- Special considerations

**Example**:
```markdown
## Implementation Details

### Test Data

Benchmarks use realistic test data including:
- {List primary test data types used}
- {Include examples of edge cases and special scenarios}

### Configuration Options Tested

- {List key configuration variations tested}
- {Include different parameter combinations}

### Iteration Counts

- CPU benchmarks: {Describe iteration strategy for CPU benchmarks}
- Memory benchmarks: {Describe iteration strategy for memory benchmarks}
```

## Content Guidelines

### Writing Style

- **Technical Accuracy**: Use precise performance terminology
- **Actionable Information**: Focus on what users need to know to run and understand benchmarks
- **Consistency**: Use consistent formatting and terminology across benchmark projects

### Performance Data

- **Include Actual Results**: Always include current baseline results
- **Use Appropriate Units**: Nanoseconds for fast operations, microseconds for typical operations, milliseconds for bulk operations
- **Context Matters**: Explain what performance numbers mean in practical terms

### Maintenance

- **Update Baselines**: Refresh performance numbers when significant changes occur
- **Version Dependencies**: Note BenchmarkDotNet version and .NET version used
- **Hardware Context**: Consider documenting test hardware characteristics

## Common Patterns for Benchmark Projects

### CPU Benchmark Classes

Use naming pattern: `{LibraryName}CpuBenchmarks`
- Focus on individual operation timing
- Test various input types and configurations
- Include edge cases and error conditions

### Memory Benchmark Classes

Use naming pattern: `{LibraryName}MemoryBenchmarks`
- Focus on allocation patterns in realistic scenarios
- Use bulk operations (1000+ iterations)
- Test collection storage and reuse patterns

### Benchmark Method Naming

Use descriptive names with underscores:
- `{OperationName}_{Scenario}_{Configuration}`
- `{BulkOperation}_{DataType}_{Option}`
- `{ProcessType}_{DataSize}_{Optimization}`

## Success Metrics

A successful benchmark README should:

- **Enable Execution**: Users can run benchmarks without additional research
- **Facilitate Understanding**: Results interpretation is clear
- **Support Maintenance**: Easy to update with new baseline results
- **Guide Development**: Helps identify performance-sensitive areas
- **Ensure Reproducibility**: Consistent setup and execution instructions

## Integration with CI/CD

Consider documenting:
- Automated benchmark execution in CI pipelines
- Performance regression detection
- Historical performance tracking
- Alert thresholds for performance degradation
