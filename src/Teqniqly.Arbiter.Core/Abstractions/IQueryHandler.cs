namespace Teqniqly.Arbiter.Core.Abstractions
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        ValueTask<TResult> Handle(TQuery query, CancellationToken ct);
    }
}
