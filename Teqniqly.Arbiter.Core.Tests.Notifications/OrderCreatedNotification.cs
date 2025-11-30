using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Tests.Notifications
{
    internal sealed record OrderCreatedNotification(Guid OrderId) : INotification;
}
