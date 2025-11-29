using Teqniqly.Arbiter.Core.Abstractions;
using Teqniqly.Arbiter.Core.Invokers;

namespace Teqniqly.Arbiter.Core
{
    /// <summary>
    /// Maintains mappings from message types to invoker delegates. The registry is populated at startup
    /// and used for fast lookup during dispatch.
    /// </summary>
    internal sealed class HandlerRegistry
    {
        private readonly Dictionary<Type, NotificationInvoker> _notify = new();
        private readonly Dictionary<RegistryKey, CQInvoker> _single = new();

        /// <summary>
        /// Register a command handler invoker for the specified command and result types.
        /// </summary>
        public void AddCommand<TCommand, TResult>()
            where TCommand : ICommand<TResult> =>
            _single[new RegistryKey(MessageKind.Command, typeof(TCommand))] = CommandInvoker<
                TCommand,
                TResult
            >.Invoke;

        /// <summary>
        /// Register a notification handler invoker for the specified notification type.
        /// </summary>
        public void AddNotification<TNotification>()
            where TNotification : INotification =>
            _notify[typeof(TNotification)] = NotificationInvoker<TNotification>.Invoke;

        /// <summary>
        /// Register a query handler invoker for the specified query and result types.
        /// </summary>
        public void AddQuery<TQuery, TResult>()
            where TQuery : IQuery<TResult> =>
            _single[new RegistryKey(MessageKind.Query, typeof(TQuery))] = QueryInvoker<
                TQuery,
                TResult
            >.Invoke;

        /// <summary>
        /// Try to get a notification invoker for the given notification type.
        /// </summary>
        public bool TryGetNotification(Type notificationType, out NotificationInvoker? invoker) =>
            _notify.TryGetValue(notificationType, out invoker);

        /// <summary>
        /// Try to get a command/query invoker for the given kind and message type.
        /// </summary>
        public bool TryGetSingle(MessageKind kind, Type messageType, out CQInvoker? invoker) =>
            _single.TryGetValue(new RegistryKey(kind, messageType), out invoker);
    }
}
