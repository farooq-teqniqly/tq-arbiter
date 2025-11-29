using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Tests.Queries
{
    internal sealed class GetOrderIdQueryHandler : IQueryHandler<GetOrderIdQuery, Guid>
    {
        public ValueTask<Guid> Handle(GetOrderIdQuery query, CancellationToken ct)
        {
            return ValueTask.FromResult(query.OrderId);
        }
    }
}
