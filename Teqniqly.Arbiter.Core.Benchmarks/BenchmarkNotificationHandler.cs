using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// First handler for BenchmarkNotification used in benchmarking.
/// </summary>
internal sealed class BenchmarkNotificationHandler : INotificationHandler<BenchmarkNotification>
{
    /// <inheritdoc />
    public ValueTask Handle(BenchmarkNotification notification, CancellationToken ct)
    {
        return ValueTask.CompletedTask;
    }
}
