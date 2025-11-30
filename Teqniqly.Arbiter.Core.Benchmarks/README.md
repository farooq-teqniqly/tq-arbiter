# Teqniqly.Arbiter.Core.Benchmarks

This project contains CPU and memory benchmarks for the Teqniqly.Arbiter.Core library using BenchmarkDotNet.

## Overview

The benchmarks measure the performance characteristics of the Teqniqly.Arbiter.Core library across various scenarios:

### ArbiterCpuBenchmarks

CPU performance benchmarks focusing on execution speed:

-   Command sending operations
-   Query asking operations
-   Notification publishing operations (with 2 handlers)
-   Message instance creation (commands, queries, notifications)

### ArbiterMemoryBenchmarks

Memory allocation benchmarks focusing on real-world usage patterns:

-   Bulk command sending (1000 operations)
-   Bulk query asking (1000 operations)
-   Bulk notification publishing (1000 operations)
-   Message creation and dispatch patterns
-   Result collection and storage

## Running the Benchmarks

### Quick Run

To run all benchmarks with default settings:

```bash
dotnet run -c Release --project src/Teqniqly.Arbiter.Core.Benchmarks/Teqniqly.Arbiter.Core.Benchmarks.csproj
```

### Run Specific Benchmark Class

To run only CPU benchmarks:

```bash
dotnet run -c Release --project src/Teqniqly.Arbiter.Core.Benchmarks/Teqniqly.Arbiter.Core.Benchmarks.csproj -- --filter *ArbiterCpuBenchmarks*
```

To run only memory benchmarks:

```bash
dotnet run -c Release --project src/Teqniqly.Arbiter.Core.Benchmarks/Teqniqly.Arbiter.Core.Benchmarks.csproj -- --filter *ArbiterMemoryBenchmarks*
```

### Run Specific Benchmark Method

To run a specific benchmark method:

```bash
dotnet run -c Release --project src/Teqniqly.Arbiter.Core.Benchmarks/Teqniqly.Arbiter.Core.Benchmarks.csproj -- --filter *Send_Command*
```

## Understanding the Results

### CPU Benchmarks

CPU benchmark results include:

-   **Mean**: Average execution time per operation
-   **Error**: Half of 99.9% confidence interval
-   **StdDev**: Standard deviation of all measurements
-   **Allocated**: Memory allocated per operation

### Memory Benchmarks

Memory benchmark results include:

-   **Gen0**: Number of Gen 0 collections per 1000 operations
-   **Gen1**: Number of Gen 1 collections per 1000 operations
-   **Allocated**: Total memory allocated

## Benchmark Configuration

The benchmarks use the following BenchmarkDotNet configurations:

-   **MemoryDiagnoser**: Enabled on all benchmarks to track memory allocations
-   **SimpleJob** (Memory benchmarks only): 3 warmup iterations, 10 measurement iterations for consistent memory measurements

## Output

Results are saved in `BenchmarkDotNet.Artifacts` directory:

-   **results/**: Contains detailed benchmark results in various formats (HTML, Markdown, CSV)
-   **logs/**: Execution logs for debugging
-   **bin/**: Compiled benchmark executables

## Performance Considerations

When analyzing benchmark results, consider:

1.  **CPU Performance**: Lower execution times indicate better performance
2.  **Memory Allocations**: Fewer allocations reduce GC pressure
3.  **GC Collections**: Fewer collections (especially Gen1+) indicate better memory efficiency
4.  **Configuration Impact**: Different mediator usage patterns have different performance characteristics
5.  **Real-world Usage**: Benchmarks reflect common usage scenarios with the mediator pattern

## Baseline Results

### CPU Benchmarks

**Key Takeaways:**

-   Individual mediator operations are highly optimized
-   Command and query dispatch show minimal overhead
-   Notification publishing handles multiple handlers efficiently
-   Message instance creation is fast due to record types

### Memory Benchmarks

**Key Takeaways:**

-   Bulk operations demonstrate allocation patterns in high-throughput scenarios
-   Pre-created message reuse reduces allocations significantly
-   Result collection follows expected patterns
-   GC pressure is minimal for typical workloads

## Best Practices for Running Benchmarks

1.  **Close unnecessary applications** to reduce system noise
2.  **Run in Release configuration** (never Debug) for accurate results
3.  **Allow benchmarks to complete** without interruption
4.  **Run multiple times** to verify consistency
5.  **Compare relative performance** rather than absolute numbers across different machines

## Contributing New Benchmarks

When adding new benchmarks:

1.  Add methods to existing benchmark classes or create new ones
2.  Use `[Benchmark]` attribute on benchmark methods
3.  Include XML documentation explaining what is being measured
4.  Use descriptive method names (underscores are allowed for readability)
5.  Follow the existing naming patterns for consistency

## Implementation Details

### Test Data

Benchmarks use realistic test data including:

-   Commands with Guid identifiers and string data
-   Queries with Guid identifiers and string data
-   Notifications with Guid identifiers and string data
-   Pre-created message instances for memory benchmarks

### Configuration Options Tested

-   Single message dispatch (commands, queries, notifications)
-   Bulk dispatch operations (1000 iterations)
-   Message reuse vs creation patterns
-   Result collection strategies

### Iteration Counts

-   CPU benchmarks: Default BenchmarkDotNet iterations for statistical accuracy
-   Memory benchmarks: 3 warmup iterations, 10 measurement iterations for consistency
-   Bulk operations: 1000 messages per benchmark iteration
