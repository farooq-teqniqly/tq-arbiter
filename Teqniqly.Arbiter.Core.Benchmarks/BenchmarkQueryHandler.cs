using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// Handler for BenchmarkQuery used in benchmarking.
/// </summary>
internal sealed class BenchmarkQueryHandler : IQueryHandler<BenchmarkQuery, string>
{
    /// <inheritdoc />
    public ValueTask<string> Handle(BenchmarkQuery query, CancellationToken ct)
    {
        return ValueTask.FromResult(query.Data);
    }
}
