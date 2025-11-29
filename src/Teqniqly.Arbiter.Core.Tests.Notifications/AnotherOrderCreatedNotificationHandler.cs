using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Tests.Notifications
{
    internal sealed class AnotherOrderCreatedNotificationHandler
        : INotificationHandler<OrderCreatedNotification>
    {
        public ValueTask Handle(OrderCreatedNotification notification, CancellationToken ct)
        {
            return ValueTask.CompletedTask;
        }
    }
}
