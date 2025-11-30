using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Extensions;

namespace Teqniqly.Arbiter.Core.Tests;

/// <summary>
/// Tests to verify that MediatorOptions.TypeFilter works correctly.
/// </summary>
public class MediatorOptionsTypeFilterTests
{
    [Fact]
    public void TypeFilter_ExcludesSpecificTypes_HandlerNotRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        var assemblies = new[]
        {
            typeof(Teqniqly.Arbiter.Core.Tests.Commands.CreateOrderCommand).Assembly,
        };

        // Act - Exclude all command handlers
        services.AddArbiter(
            opts =>
            {
                opts.TypeFilter = t => !t.Name.Contains("CommandHandler", StringComparison.Ordinal);
            },
            assemblies
        );

        // Assert - CommandHandler service descriptors should not be registered
        var commandHandlerDescriptors = services
            .Where(sd => sd.ServiceType.Name.Contains("ICommandHandler", StringComparison.Ordinal))
            .ToList();

        Assert.Empty(commandHandlerDescriptors);
    }

    [Fact]
    public void TypeFilter_FiltersHandlerTypes_DuringRegistration()
    {
        // Arrange
        var services = new ServiceCollection();

        // Get assemblies that contain only the handlers we want to test
        var assemblies = new[]
        {
            typeof(Teqniqly.Arbiter.Core.Tests.Commands.CreateOrderCommand).Assembly,
            typeof(Teqniqly.Arbiter.Core.Tests.Queries.GetOrderIdQuery).Assembly,
            typeof(Teqniqly.Arbiter.Core.Tests.Notifications.OrderCreatedNotification).Assembly,
        };

        // Act - Configure to filter by a specific criteria
        var filteredCount = 0;
        services.AddArbiter(
            opts =>
            {
                opts.TypeFilter = t =>
                {
                    // Count how many types are being filtered
                    if (t.Name.Contains("Handler", StringComparison.Ordinal))
                    {
                        filteredCount++;
                    }
                    return true; // Include all for this test
                };
            },
            assemblies
        );

        // Assert - TypeFilter was called
        Assert.True(filteredCount > 0, "TypeFilter should have been called with handler types");
    }

    [Fact]
    public void TypeFilter_WhenNull_IncludesAllHandlerTypes()
    {
        // Arrange
        var services = new ServiceCollection();

        var assemblies = new[]
        {
            typeof(Teqniqly.Arbiter.Core.Tests.Commands.CreateOrderCommand).Assembly,
        };

        // Act - Use null TypeFilter (default)
        services.AddArbiter(
            opts =>
            {
                opts.TypeFilter = null;
            },
            assemblies
        );

        // Assert - Handler should be registered
        var commandHandlerDescriptors = services
            .Where(sd => sd.ServiceType.Name.Contains("ICommandHandler", StringComparison.Ordinal))
            .ToList();

        Assert.NotEmpty(commandHandlerDescriptors);
    }

    [Fact]
    public void TypeFilter_WithCustomPredicate_FiltersBasedOnTypeProperties()
    {
        // Arrange
        var services = new ServiceCollection();

        var assemblies = new[]
        {
            typeof(Teqniqly.Arbiter.Core.Tests.Commands.CreateOrderCommand).Assembly,
            typeof(Teqniqly.Arbiter.Core.Tests.Queries.GetOrderIdQuery).Assembly,
        };

        var publicTypeCount = 0;
        var internalTypeCount = 0;

        // Act - Count public vs internal types
        services.AddArbiter(
            opts =>
            {
                opts.TypeFilter = t =>
                {
                    if (t.IsPublic || t.IsNestedPublic)
                    {
                        publicTypeCount++;
                    }
                    else
                    {
                        internalTypeCount++;
                    }
                    return true;
                };
            },
            assemblies
        );

        // Assert - Both public and internal types should be encountered
        Assert.True(internalTypeCount > 0, "Internal types should be scanned");
    }
}
