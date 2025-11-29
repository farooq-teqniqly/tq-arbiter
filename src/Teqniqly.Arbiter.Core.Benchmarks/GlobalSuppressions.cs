using System.Diagnostics.CodeAnalysis;

// Benchmark methods with underscores for readability are acceptable
[assembly: SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "Benchmark method names use underscores for readability"
)]

// Benchmark classes must be public for BenchmarkDotNet to discover them
[assembly: SuppressMessage(
    "Design",
    "CA1515:Consider making public types internal",
    Justification = "Benchmark types must be public for BenchmarkDotNet"
)]

// Benchmark methods must be instance methods, not static
[assembly: SuppressMessage(
    "Performance",
    "CA1822:Mark members as static",
    Justification = "Benchmark methods must be instance methods for BenchmarkDotNet"
)]

// List<T> is appropriate for benchmark methods measuring collection performance
[assembly: SuppressMessage(
    "Design",
    "CA1002:Do not expose generic lists",
    Justification = "Benchmark methods return lists to measure collection performance"
)]

// Async methods without cancellation token support is acceptable for benchmarks
[assembly: SuppressMessage(
    "Design",
    "CA1068:CancellationToken parameters must come last",
    Justification = "BenchmarkDotNet methods don't use cancellation tokens"
)]

// ConfigureAwait is not necessary in console application benchmarks
[assembly: SuppressMessage(
    "Reliability",
    "CA2007:Consider calling ConfigureAwait on the awaited task",
    Justification = "ConfigureAwait is not necessary in console benchmark applications"
)]
