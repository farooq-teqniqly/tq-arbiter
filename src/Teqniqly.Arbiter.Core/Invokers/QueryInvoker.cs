using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Invokers
{
    /// <summary>
    /// Builds a strongly-typed invoker for a query type.
    /// </summary>
    internal static class QueryInvoker<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Invokes the resolved <see cref="IQueryHandler{TQuery, TResult}"/> from DI.
        /// </summary>
        public static readonly CQInvoker Invoke = async (sp, msg, ctx, ct) =>
        {
            var h = sp.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await h.Handle((TQuery)msg, ct);
        };
    }
}