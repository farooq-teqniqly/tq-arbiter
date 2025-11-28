using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;
using Teqniqly.Arbiter.Core.Extensions;

namespace Teqniqly.Arbiter.Core.Tests
{
    public class DefaultMediatorTests
    {
        [Fact]
        public async Task Can_Route_Command_To_Handler()
        {
            var sc = new ServiceCollection();
            sc.AddScoped<ICommandHandler<CreateOrderCommand, Guid>, CreateOrderCommandHandler>();
            sc.AddArbiter(null, typeof(CreateOrderCommandHandler).Assembly);

            var mediator = sc.BuildServiceProvider().GetRequiredService<IMediator>();

            var expectedOrderId = Guid.NewGuid();
            var actualOrderId = await mediator.Send(new CreateOrderCommand(expectedOrderId));

            Assert.Equal(expectedOrderId, actualOrderId);
        }

        [Fact]
        public async Task Can_Route_Query_To_Handler()
        {
            var sc = new ServiceCollection();
            sc.AddScoped<IQueryHandler<GetOrderIdQuery, Guid>, GetOrderIdQueryHandler>();
            sc.AddArbiter(null, typeof(GetOrderIdQueryHandler).Assembly);

            var mediator = sc.BuildServiceProvider().GetRequiredService<IMediator>();

            var expectedOrderId = Guid.NewGuid();
            var actualOrderId = await mediator.Ask(new GetOrderIdQuery(expectedOrderId));

            Assert.Equal(expectedOrderId, actualOrderId);
        }

        [Fact]

    }

    internal sealed record GetOrderIdQuery(Guid OrderId) : IQuery<Guid>;

    internal sealed class GetOrderIdQueryHandler : IQueryHandler<GetOrderIdQuery, Guid>
    {
        public ValueTask<Guid> Handle(GetOrderIdQuery query, CancellationToken ct)
        {
            return ValueTask.FromResult(query.OrderId);
        }
    }

    internal sealed record CreateOrderCommand(Guid OrderId) : ICommand<Guid>;

    internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
    {
        public ValueTask<Guid> Handle(CreateOrderCommand command, CancellationToken ct)
        {
            return ValueTask.FromResult(command.OrderId);
        }
    }
}
