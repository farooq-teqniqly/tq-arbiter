using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// Simple query used for benchmarking.
/// </summary>
public sealed record BenchmarkQuery(Guid Id, string Data) : IQuery<string>;
