using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Tests.Commands
{
    internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
    {
        public ValueTask<Guid> Handle(CreateOrderCommand command, CancellationToken ct)
        {
            return ValueTask.FromResult(command.OrderId);
        }
    }
}
