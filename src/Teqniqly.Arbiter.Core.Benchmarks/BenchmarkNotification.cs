using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// Simple notification used for benchmarking.
/// </summary>
public sealed record BenchmarkNotification(Guid Id, string Data) : INotification;
