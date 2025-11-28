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

-   **Project Type**: Console application with `<OutputType>Exe</OutputType>`
-   **Package Reference**: Include BenchmarkDotNet NuGet package
-   **Project References**: Reference the library being benchmarked
-   **CLS Compliance**: Include AssemblyInfo.cs with CLSCompliant attribute

### File Organization

-   **Separate Concerns**: Create distinct benchmark classes for different aspects:
    -   CPU benchmarks: Focus on execution time of individual operations
    -   Memory benchmarks: Focus on allocation patterns and real-world usage scenarios
-   **Avoid Duplication**: Ensure benchmark classes test different scenarios, not the same operations
-   **Helper Types**: Create concrete test types (e.g., test error classes) in separate files

## Benchmark Class Design

### CPU Benchmarks

-   **Purpose**: Measure execution time of individual operations
-   **Method Signatures**: Return values from benchmark methods
-   **Scope**: Test atomic operations (single calls, property access, etc.)
-   **Attributes**: Use `[MemoryDiagnoser]` to track per-operation allocations
-   **Naming**: Use descriptive names with underscores for readability (e.g., `CreateSuccessResult_String`)

### Memory Benchmarks

-   **Purpose**: Measure allocation patterns in realistic scenarios
-   **Method Signatures**: Can be void or return collections/results as appropriate
-   **Scope**: Test bulk operations (hundreds or thousands of iterations)
-   **Focus Areas**:
    -   Object reuse vs creation
    -   Collection storage patterns
    -   LINQ operations
    -   Chained operations
    -   Large payloads
-   **Attributes**: Use `[MemoryDiagnoser]` and optionally `[SimpleJob]` for consistent measurements
-   **Iteration Count**: Use constants (e.g., 1000) for bulk operation counts

### Benchmark Method Guidelines

-   **Public Access**: Methods must be public for BenchmarkDotNet to discover them
-   **Instance Methods**: Methods must be instance methods (not static)
-   **[Benchmark] Attribute**: Apply to all benchmark methods
-   **XML Documentation**: Include comprehensive documentation explaining what is being measured
-   **Setup/Cleanup**: Use `[GlobalSetup]`, `[GlobalCleanup]`, `[IterationSetup]`, `[IterationCleanup]` as needed

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

-   Creating success results with different types (reference types, value types, Unit)
-   Creating failure results with different types
-   Accessing properties (IsSuccess, IsFailure)
-   Retrieving values (GetValue, GetError)

### Real-World Patterns

-   Storing results in collections (arrays, lists, dictionaries)
-   Filtering results using LINQ
-   Chaining result operations
-   Nested results
-   Large payload handling
-   Object reuse vs new instance creation

### Performance Optimization Testing

-   Comparing reused instances vs new allocations
-   Shared error instances vs unique errors
-   Shared values vs unique values
-   Collection growth patterns

## Documentation Requirements

### Benchmark Project README

Include the following sections:

1.  **Overview**: Explain what aspects are being benchmarked
2.  **Benchmark Classes**: Describe each benchmark class and its purpose
3.  **Running Instructions**: Provide commands for:


    -   Running all benchmarks
    -   Running specific benchmark classes
    -   Running specific benchmark methods

4.  **Understanding Results**: Explain key metrics (Mean, StdDev, Allocated, Gen0/1/2)
5.  **Configuration Details**: Document BenchmarkDotNet attributes and settings used
6.  **Best Practices**: Include tips for running benchmarks (Release mode, reducing noise, etc.)
7.  **Contributing**: Guidelines for adding new benchmarks

### Main Repository README

-   Add a "Performance Benchmarks" section
-   Link to the benchmarks README
-   Briefly describe what benchmarks are available

## Program.cs Structure

Keep it simple and run all benchmark classes:

```csharp
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<CpuBenchmarks>();
BenchmarkRunner.Run<MemoryBenchmarks>();
```

## Common Pitfalls to Avoid

-   **Duplicate Tests**: Don't create memory benchmarks that test the same operations as CPU benchmarks
-   **Static Methods**: Benchmark methods must be instance methods
-   **Missing Documentation**: Always document what is being measured and why
-   **Inconsistent Patterns**: Follow established naming and organization patterns
-   **Ignoring GC**: Use MemoryDiagnoser to track allocations and GC pressure
-   **Debug Mode**: Always run benchmarks in Release configuration
-   **Noisy Environment**: Run benchmarks in a quiet environment (close other applications)

## Naming Conventions

-   **Benchmark Classes**: Use descriptive names ending with "Benchmarks" (e.g., `ResultCpuBenchmarks`, `ResultMemoryBenchmarks`)
-   **Benchmark Methods**: Use descriptive names with underscores for readability
-   **Helper Types**: Use descriptive names suffixed with purpose (e.g., `BenchmarkError`)
-   **Constants**: Use UPPERCASE for iteration counts and test data

## Testing Benchmarks

Before committing benchmark code:

1.  Verify the project builds cleanly (`dotnet build --configuration Release`)
2.  Run a quick benchmark to ensure no runtime errors (`dotnet run -c Release`)
3.  Review benchmark output for unexpected results
4.  Ensure all code analysis warnings are appropriately suppressed
5.  Verify XML documentation is complete and accurate
