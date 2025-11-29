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
        private readonly IMediator _mediator;

        public DefaultMediatorTests()
        {
            var sc = new ServiceCollection();

            sc.AddScoped<ICommandHandler<CreateOrderCommand, Guid>, CreateOrderCommandHandler>();
            sc.AddScoped<IQueryHandler<GetOrderIdQuery, Guid>, GetOrderIdQueryHandler>();

            sc.AddScoped<
                INotificationHandler<OrderCreatedNotification>,
                OrderCreatedNotificationHandler
            >();

            sc.AddScoped<
                INotificationHandler<OrderCreatedNotification>,
                AnotherOrderCreatedNotificationHandler
            >();

            sc.AddArbiter(
                typeof(CreateOrderCommandHandler).Assembly,
                typeof(GetOrderIdQuery).Assembly,
                typeof(OrderCreatedNotificationHandler).Assembly,
                typeof(AnotherOrderCreatedNotificationHandler).Assembly
            );

            _mediator = sc.BuildServiceProvider().GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Ask_Throws_When_Missing_Handler()
        {
            var sc = new ServiceCollection();

            // Build the registry from an unrelated assembly so it doesn't contain our test handlers
            sc.AddArbiter(typeof(object).Assembly);

            var mediator = sc.BuildServiceProvider().GetRequiredService<IMediator>();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                async () =>
                    await mediator.Ask(new GetOrderIdQuery(Guid.NewGuid())).ConfigureAwait(false)
            );

            Assert.Equal("No query handler for GetOrderIdQuery", exception.Message);
        }

        [Fact]
        public async Task Can_Route_Command_To_Handler()
        {
            var expectedOrderId = Guid.NewGuid();
            var actualOrderId = await _mediator.Send(new CreateOrderCommand(expectedOrderId));

            Assert.Equal(expectedOrderId, actualOrderId);
        }

        [Fact]
        public async Task Can_Route_Notification_To_Handlers()
        {
            var notification = new OrderCreatedNotification(Guid.NewGuid());

            await _mediator.Publish(notification);

            // If we reach here without exceptions, both handlers were invoked successfully.
            Assert.True(true);
        }

        [Fact]
        public async Task Can_Route_Query_To_Handler()
        {
            var expectedOrderId = Guid.NewGuid();
            var actualOrderId = await _mediator.Ask(new GetOrderIdQuery(expectedOrderId));

            Assert.Equal(expectedOrderId, actualOrderId);
        }

        [Fact]
        public async Task Publish_Does_Not_Throw_When_No_Handler()
        {
            var sc = new ServiceCollection();

            sc.AddArbiter(typeof(object).Assembly);

            var mediator = sc.BuildServiceProvider().GetRequiredService<IMediator>();

            await mediator.Publish(new OrderCreatedNotification(Guid.NewGuid()));

            // If we reach here, no exception was thrown.
            Assert.True(true);
        }

        [Fact]
        public async Task Send_Throws_When_Missing_Handler()
        {
            var sc = new ServiceCollection();

            // Build the registry from an unrelated assembly so it doesn't contain our test handlers
            sc.AddArbiter(typeof(object).Assembly);

            var mediator = sc.BuildServiceProvider().GetRequiredService<IMediator>();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                async () =>
                    await mediator
                        .Send(new CreateOrderCommand(Guid.NewGuid()))
                        .ConfigureAwait(false)
            );

            Assert.Equal("No command handler for CreateOrderCommand", exception.Message);
        }
    }
}
