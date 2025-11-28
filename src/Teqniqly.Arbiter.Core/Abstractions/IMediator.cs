namespace Teqniqly.Arbiter.Core.Abstractions
{
    public interface IMediator
    {
        ValueTask<TResult> Ask<TResult>(IQuery<TResult> query, CancellationToken ct = default);
        ValueTask Publish<TNotification>(TNotification notification, CancellationToken ct = default)
            where TNotification : INotification;
        ValueTask<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct = default);
    }
}
