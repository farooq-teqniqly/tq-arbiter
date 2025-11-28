using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Invokers
{
    internal static class QueryInvoker<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public static readonly CQInvoker Invoke = async (sp, msg, ctx, ct) =>
        {
            var h = sp.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await h.Handle((TQuery)msg, ct);
        };
    }
}