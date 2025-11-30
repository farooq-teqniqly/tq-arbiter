using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Tests.Commands
{
    internal sealed record CreateOrderCommand(Guid OrderId) : ICommand<Guid>;
}
