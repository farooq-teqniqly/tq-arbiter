namespace Teqniqly.Arbiter.Core.Abstractions
{
    /// <summary>
    /// Marker interface for a query that produces a result of type <typeparamref name="TResult"/>.
    /// Queries represent intent to retrieve data without side effects.
    /// </summary>
    /// <typeparam name="TResult">The result type returned by the query.</typeparam>
    public interface IQuery<TResult> { }
}
