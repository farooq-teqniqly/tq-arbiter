using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Invokers
{
    /// <summary>
    /// Delegate used to invoke notification handlers.
    /// </summary>
    internal delegate ValueTask NotificationInvoker(
        IServiceProvider sp,
        object message,
        MessageContext ctx,
        CancellationToken ct
    );

    /// <summary>
    /// Builds a strongly-typed invoker for a notification type that executes all registered handlers.
    /// </summary>
    internal static class NotificationInvoker<TNotification>
        where TNotification : INotification
    {
        /// <summary>
        /// Invokes all resolved <see cref="INotificationHandler{TNotification}"/> instances from DI.
        /// </summary>
        public static readonly NotificationInvoker Invoke = async (sp, msg, _, ct) =>
        {
            if (msg is not TNotification typedMsg)
            {
                throw new InvalidOperationException(
                    $"Expected notification of type '{typeof(TNotification).FullName}' "
                        + $"but received '{msg?.GetType().FullName ?? "null"}'. "
                        + "This indicates a registry misconfiguration."
                );
            }

            foreach (var h in sp.GetServices<INotificationHandler<TNotification>>())
            {
                await h.Handle(typedMsg, ct);
            }
        };
    }
}
