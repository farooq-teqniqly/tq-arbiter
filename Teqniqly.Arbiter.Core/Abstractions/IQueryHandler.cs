namespace Teqniqly.Arbiter.Core.Abstractions
{
    /// <summary>
    /// Handler for a query of type <typeparamref name="TQuery"/> that returns <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TQuery">The query type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handle the specified query.
        /// </summary>
        /// <param name="query">The query instance.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The result produced by handling the query.</returns>
        ValueTask<TResult> Handle(TQuery query, CancellationToken ct);
    }
}
