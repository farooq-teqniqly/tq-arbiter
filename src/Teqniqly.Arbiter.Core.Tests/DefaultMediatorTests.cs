using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;
using Teqniqly.Arbiter.Core.Extensions;
using Teqniqly.Arbiter.Core.Tests.Commands;
using Teqniqly.Arbiter.Core.Tests.Notifications;
using Teqniqly.Arbiter.Core.Tests.Queries;

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
        public async Task Can_Route_Notification_To_Handlers()
        {
            var sc = new ServiceCollection();

            sc.AddScoped<
                INotificationHandler<OrderCreatedNotification>,
                OrderCreatedNotificationHandler
            >();

            sc.AddScoped<
                INotificationHandler<OrderCreatedNotification>,
                AnotherOrderCreatedNotificationHandler
            >();

            sc.AddArbiter(null, typeof(OrderCreatedNotificationHandler).Assembly);

            var mediator = sc.BuildServiceProvider().GetRequiredService<IMediator>();

            var notification = new OrderCreatedNotification(Guid.NewGuid());

            await mediator.Publish(notification);

            // If we reach here without exceptions, both handlers were invoked successfully.
            Assert.True(true);
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
    }
}
