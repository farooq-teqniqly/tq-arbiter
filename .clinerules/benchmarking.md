---
description: Guidelines and best practices for creating and maintaining benchmarks
author: team
version: 1.0
globs: ["**/*Benchmarks.csproj", "**/*Benchmarks/**/*.cs"]
tags: ["benchmarking", "performance", "best-practices"]
---

# Benchmarking Guidelines

## Benchmark Project Structure

### Project Configuration

- **Project Type**: Console application with `<OutputType>Exe</OutputType>`
- **Package Reference**: Include BenchmarkDotNet NuGet package
- **Project References**: Reference the library being benchmarked
- **CLS Compliance**: Include AssemblyInfo.cs with CLSCompliant attribute

### File Organization

- **Separate Concerns**: Create distinct benchmark classes for different aspects:
  - CPU benchmarks: Focus on execution time of individual operations
  - Memory benchmarks: Focus on allocation patterns and real-world usage scenarios
- **Avoid Duplication**: Ensure benchmark classes test different scenarios, not the same operations
- **Helper Types**: Create concrete test types (e.g., test error classes) in separate files

## Benchmark Class Design

### CPU Benchmarks

- **Purpose**: Measure execution time of individual operations
- **Method Signatures**: Return values from benchmark methods
- **Scope**: Test atomic operations (single calls, property access, etc.)
- **Attributes**: Use `[MemoryDiagnoser]` to track per-operation allocations
- **Naming**: Use descriptive names with underscores for readability (e.g., `CreateSuccessResult_String`)

### Memory Benchmarks

- **Purpose**: Measure allocation patterns in realistic scenarios
- **Method Signatures**: Can be void or return collections/results as appropriate
- **Scope**: Test bulk operations (hundreds or thousands of iterations)
- **Focus Areas**:
  - Object reuse vs creation
  - Collection storage patterns
  - LINQ operations
  - Chained operations
  - Large payloads
- **Attributes**: Use `[MemoryDiagnoser]` and optionally `[SimpleJob]` for consistent measurements
- **Iteration Count**: Use constants (e.g., 1000) for bulk operation counts

### Benchmark Method Guidelines

- **Public Access**: Methods must be public for BenchmarkDotNet to discover them
- **Instance Methods**: Methods must be instance methods (not static)
- **[Benchmark] Attribute**: Apply to all benchmark methods
- **XML Documentation**: Include comprehensive documentation explaining what is being measured
- **Setup/Cleanup**: Use `[GlobalSetup]`, `[GlobalCleanup]`, `[IterationSetup]`, `[IterationCleanup]` as needed

## CI Configuration

### CiConfig Class

Benchmark projects should include a `CiConfig` class optimized for running benchmarks in CI/CD environments:

- **Purpose**: Provide a lightweight benchmark configuration that runs quickly in CI pipelines while still providing meaningful performance data
- **Location**: Create as a separate file `CiConfig.cs` in the benchmark project root
- **Usage**: Applied to benchmark classes to optimize for CI execution when running automated performance tests

#### CiConfig Implementation

```csharp
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

/// <summary>
/// Benchmark configuration optimized for CI/CD environments.
/// Uses minimal iterations to provide fast feedback while maintaining statistical relevance.
/// </summary>
public class CiConfig : ManualConfig
{
    public CiConfig()
    {
        AddJob(
                Job.Default.WithRuntime(CoreRuntime.Core90)
                    .WithPlatform(Platform.X64)
                    .WithJit(Jit.RyuJit)
                    .WithWarmupCount(5) // More warmup to stabilize tiered JIT
                    .WithIterationTime(TimeInterval.FromMilliseconds(500))
                    .WithIterationCount(20) // More iterations for tighter confidence intervals
                    .WithGcServer(true)
                    .WithGcConcurrent(true)
                    .WithId("CI")
            );

            AddDiagnoser(MemoryDiagnoser.Default);
            WithOptions(ConfigOptions.JoinSummary);
    }
}
```

#### Key Configuration Principles

- **Minimal Warmup**: Use 1 warmup iteration to reduce total execution time
- **Reduced Iterations**: Use 3-5 measurement iterations (vs. 10-15 for local runs)
- **Statistical Relevance**: Balance speed with sufficient data for regression detection
- **Consistency**: Use the same configuration across all CI benchmark runs for comparable results

#### Application to Benchmark Classes

Apply the CiConfig to benchmark classes when running in CI:

```csharp
#if CI_BUILD
[Config(typeof(CiConfig))]
#endif
public class MyBenchmarks
{
    // Benchmark methods...
}
```

Or use environment-based detection:

```csharp
[Config(typeof(CiConfig))]
[Condition(nameof(IsRunningInCi))]
public class MyBenchmarks
{
    public static bool IsRunningInCi =>
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));

    // Benchmark methods...
}
```

#### CI vs Local Execution

- **CI Execution**: Fast feedback, regression detection, trend tracking
- **Local Execution**: Detailed analysis, optimization work, baseline establishment
- **Separate Baselines**: Consider maintaining separate baselines for CI vs local execution due to different configurations

## Code Analysis Suppressions

Create a GlobalSuppressions.cs file with appropriate suppressions:

```csharp
// Benchmark methods with underscores for readability are acceptable
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]

// Benchmark classes must be public for BenchmarkDotNet to discover them
[assembly: SuppressMessage("Design", "CA1515:Consider making public types internal")]

// Benchmark methods must be instance methods, not static
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static")]

// List<T> is appropriate for benchmark methods measuring collection performance
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists")]
```

## Benchmark Scenarios to Consider

### Basic Operations

- Creating success results with different types (reference types, value types, Unit)
- Creating failure results with different types
- Accessing properties (IsSuccess, IsFailure)
- Retrieving values (GetValue, GetError)

### Real-World Patterns

- Storing results in collections (arrays, lists, dictionaries)
- Filtering results using LINQ
- Chaining result operations
- Nested results
- Large payload handling
- Object reuse vs new instance creation

### Performance Optimization Testing

- Comparing reused instances vs new allocations
- Shared error instances vs unique errors
- Shared values vs unique values
- Collection growth patterns

## Documentation Requirements

### Benchmark Project README

Include the following sections:

1.  **Overview**: Explain what aspects are being benchmarked
2.  **Benchmark Classes**: Describe each benchmark class and its purpose
3.  **Running Instructions**: Provide commands for:

    - Running all benchmarks
    - Running specific benchmark classes
    - Running specific benchmark methods

4.  **Understanding Results**: Explain key metrics (Mean, StdDev, Allocated, Gen0/1/2)
5.  **Configuration Details**: Document BenchmarkDotNet attributes and settings used
6.  **Best Practices**: Include tips for running benchmarks (Release mode, reducing noise, etc.)
7.  **Contributing**: Guidelines for adding new benchmarks

### Main Repository README

- Add a "Performance Benchmarks" section
- Link to the benchmarks README
- Briefly describe what benchmarks are available

## Program.cs Structure

Keep it simple and run all benchmark classes:

```csharp
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<CpuBenchmarks>();
BenchmarkRunner.Run<MemoryBenchmarks>();
```

## Common Pitfalls to Avoid

- **Duplicate Tests**: Don't create memory benchmarks that test the same operations as CPU benchmarks
- **Static Methods**: Benchmark methods must be instance methods
- **Missing Documentation**: Always document what is being measured and why
- **Inconsistent Patterns**: Follow established naming and organization patterns
- **Ignoring GC**: Use MemoryDiagnoser to track allocations and GC pressure
- **Debug Mode**: Always run benchmarks in Release configuration
- **Noisy Environment**: Run benchmarks in a quiet environment (close other applications)

## Naming Conventions

- **Benchmark Classes**: Use descriptive names ending with "Benchmarks" (e.g., `ResultCpuBenchmarks`, `ResultMemoryBenchmarks`)
- **Benchmark Methods**: Use descriptive names with underscores for readability
- **Helper Types**: Use descriptive names suffixed with purpose (e.g., `BenchmarkError`)
- **Constants**: Use UPPERCASE for iteration counts and test data

## Testing Benchmarks

Before committing benchmark code:

1.  Verify the project builds cleanly (`dotnet build --configuration Release`)
2.  Run a quick benchmark to ensure no runtime errors (`dotnet run -c Release`)
3.  Review benchmark output for unexpected results
4.  Ensure all code analysis warnings are appropriately suppressed
5.  Verify XML documentation is complete and accurate
