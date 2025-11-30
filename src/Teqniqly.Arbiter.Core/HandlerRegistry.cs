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
        /// <exception cref="InvalidOperationException">Thrown when a duplicate command handler is registered.</exception>
        public void AddCommand<TCommand, TResult>()
            where TCommand : ICommand<TResult>
        {
            var key = new RegistryKey(MessageKind.Command, typeof(TCommand));

            if (_single.ContainsKey(key))
            {
                throw new InvalidOperationException(
                    $"Duplicate command handler registration detected for command type '{typeof(TCommand).Name}'."
                );
            }

            _single[key] = CommandInvoker<TCommand, TResult>.Invoke;
        }

        /// <summary>
        /// Register a notification handler invoker for the specified notification type.
        /// </summary>
        public void AddNotification<TNotification>()
            where TNotification : INotification =>
            _notify[typeof(TNotification)] = NotificationInvoker<TNotification>.Invoke;

        /// <summary>
        /// Register a query handler invoker for the specified query and result types.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when a duplicate query handler is registered.</exception>
        public void AddQuery<TQuery, TResult>()
            where TQuery : IQuery<TResult>
        {
            var key = new RegistryKey(MessageKind.Query, typeof(TQuery));

            if (_single.ContainsKey(key))
            {
                throw new InvalidOperationException(
                    $"Duplicate query handler registration detected for query type '{typeof(TQuery).Name}'."
                );
            }

            _single[key] = QueryInvoker<TQuery, TResult>.Invoke;
        }

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
