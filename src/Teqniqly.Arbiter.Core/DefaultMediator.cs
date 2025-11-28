using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core
{
    internal sealed class DefaultMediator : IMediator
    {
        private readonly IMessageContextAccessor _ctx;
        private readonly HandlerRegistry _registry;
        private readonly IServiceProvider _sp;

        public DefaultMediator(
            IServiceProvider sp,
            HandlerRegistry registry,
            IMessageContextAccessor ctx
        ) => (_sp, _registry, _ctx) = (sp, registry, ctx);

        public async ValueTask<TResult> Ask<TResult>(
            IQuery<TResult> query,
            CancellationToken ct = default
        )
        {
            if (
                !_registry.TryGetSingle(MessageKind.Query, query.GetType(), out var inv)
                || inv is null
            )
            {
                throw new InvalidOperationException($"No query handler for {query.GetType().Name}");
            }

            var ctx = _ctx.Current ?? MessageContextDefaults.New();
            return (TResult)(await inv(_sp, query, ctx, ct).ConfigureAwait(false))!;
        }

        public async ValueTask Publish<TNotification>(
            TNotification notification,
            CancellationToken ct = default
        )
            where TNotification : INotification
        {
            if (!_registry.TryGetNotification(notification.GetType(), out var inv) || inv is null)
            {
                return;
            }

            var ctx = _ctx.Current ?? MessageContextDefaults.New();
            await inv(_sp, notification, ctx, ct).ConfigureAwait(false);
        }

        public async ValueTask<TResult> Send<TResult>(
            ICommand<TResult> command,
            CancellationToken ct = default
        )
        {
            if (
                !_registry.TryGetSingle(MessageKind.Command, command.GetType(), out var inv)
                || inv is null
            )
            {
                throw new InvalidOperationException(
                    $"No command handler for {command.GetType().Name}"
                );
            }

            var ctx = _ctx.Current ?? MessageContextDefaults.New();
            return (TResult)(await inv(_sp, command, ctx, ct).ConfigureAwait(false))!;
        }
    }
}
