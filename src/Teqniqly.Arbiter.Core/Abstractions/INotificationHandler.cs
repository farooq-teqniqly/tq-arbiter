namespace Teqniqly.Arbiter.Core.Abstractions
{
    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        ValueTask Handle(TNotification notification, CancellationToken ct);
    }
}
