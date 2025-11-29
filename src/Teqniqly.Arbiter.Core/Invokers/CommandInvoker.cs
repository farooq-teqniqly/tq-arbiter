using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Invokers
{
    /// <summary>
    /// Delegate used to invoke command/query handlers returning an object.
    /// </summary>
    internal delegate ValueTask<object?> CQInvoker(
        IServiceProvider sp,
        object message,
        MessageContext ctx,
        CancellationToken ct
    );

    /// <summary>
    /// Builds a strongly-typed invoker for a command type.
    /// </summary>
    internal static class CommandInvoker<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        /// <summary>
        /// Invokes the resolved <see cref="ICommandHandler{TCommand, TResult}"/> from DI.
        /// </summary>
        public static readonly CQInvoker Invoke = async (sp, msg, ctx, ct) =>
        {
            var h = sp.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await h.Handle((TCommand)msg, ct);
        };
    }
}
