using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Tests.Queries
{
    internal sealed record GetOrderIdQuery(Guid OrderId) : IQuery<Guid>;
}
