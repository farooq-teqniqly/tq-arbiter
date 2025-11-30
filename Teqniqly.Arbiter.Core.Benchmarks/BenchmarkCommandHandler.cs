using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// Handler for BenchmarkCommand used in benchmarking.
/// </summary>
internal sealed class BenchmarkCommandHandler : ICommandHandler<BenchmarkCommand, Guid>
{
    /// <inheritdoc />
    public ValueTask<Guid> Handle(BenchmarkCommand command, CancellationToken ct)
    {
        return ValueTask.FromResult(command.Id);
    }
}
