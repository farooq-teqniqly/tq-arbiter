using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Invokers
{
    internal delegate ValueTask<object?> CQInvoker(
        IServiceProvider sp,
        object message,
        MessageContext ctx,
        CancellationToken ct
    );

    internal static class CommandInvoker<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        public static readonly CQInvoker Invoke = async (sp, msg, ctx, ct) =>
        {
            var h = sp.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await h.Handle((TCommand)msg, ct);
        };
    }
}
