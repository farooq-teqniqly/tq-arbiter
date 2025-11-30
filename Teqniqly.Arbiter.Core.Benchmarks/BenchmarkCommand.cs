using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// Simple command used for benchmarking.
/// </summary>
public sealed record BenchmarkCommand(Guid Id, string Data) : ICommand<Guid>;
