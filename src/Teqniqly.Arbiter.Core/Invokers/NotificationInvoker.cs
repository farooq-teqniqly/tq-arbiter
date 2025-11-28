using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Invokers
{
    internal delegate ValueTask NotificationInvoker(
        IServiceProvider sp,
        object message,
        MessageContext ctx,
        CancellationToken ct
    );

    internal static class NotificationInvoker<TNotification>
        where TNotification : INotification
    {
        public static readonly NotificationInvoker Invoke = async (sp, msg, ctx, ct) =>
        {
            foreach (var h in sp.GetServices<INotificationHandler<TNotification>>())
                await h.Handle((TNotification)msg, ct);
        };
    }
}
