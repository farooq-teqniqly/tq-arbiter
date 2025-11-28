using Teqniqly.Arbiter.Core.Abstractions;
using Teqniqly.Arbiter.Core.Invokers;

namespace Teqniqly.Arbiter.Core
{
    internal sealed class HandlerRegistry
    {
        private readonly Dictionary<Type, NotificationInvoker> _notify = new();
        private readonly Dictionary<RegistryKey, CQInvoker> _single = new();

        public void AddCommand<TCommand, TResult>()
            where TCommand : ICommand<TResult> =>
            _single[new(MessageKind.Command, typeof(TCommand))] = CommandInvoker<
                TCommand,
                TResult
            >.Invoke;

        public void AddNotification<TNotification>()
            where TNotification : INotification =>
            _notify[typeof(TNotification)] = NotificationInvoker<TNotification>.Invoke;

        public void AddQuery<TQuery, TResult>()
            where TQuery : IQuery<TResult> =>
            _single[new(MessageKind.Query, typeof(TQuery))] = QueryInvoker<TQuery, TResult>.Invoke;

        public bool TryGetNotification(Type notificationType, out NotificationInvoker? invoker) =>
            _notify.TryGetValue(notificationType, out invoker);

        public bool TryGetSingle(MessageKind kind, Type messageType, out CQInvoker? invoker) =>
            _single.TryGetValue(new(kind, messageType), out invoker);
    }
}
