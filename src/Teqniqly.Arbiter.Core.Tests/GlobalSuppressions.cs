// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this test project.
using System.Diagnostics.CodeAnalysis;

// Suppress CA1812 for test handler classes instantiated by the test DI container or used by reflection
[assembly: SuppressMessage(
    "Microsoft.Performance",
    "CA1812:AvoidUninstantiatedInternalClasses",
    Justification = "Test handlers are instantiated by the test DI container or used via reflection in tests.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Tests.Queries.GetOrderIdQueryHandler"
)]

[assembly: SuppressMessage(
    "Microsoft.Performance",
    "CA1812:AvoidUninstantiatedInternalClasses",
    Justification = "Test handlers are instantiated by the test DI container or used via reflection in tests.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Tests.Notifications.AnotherOrderCreatedNotificationHandler"
)]

[assembly: SuppressMessage(
    "Microsoft.Performance",
    "CA1812:AvoidUninstantiatedInternalClasses",
    Justification = "Test handlers are instantiated by the test DI container or used via reflection in tests.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Tests.Commands.CreateOrderCommandHandler"
)]

[assembly: SuppressMessage(
    "Microsoft.Performance",
    "CA1812:AvoidUninstantiatedInternalClasses",
    Justification = "Test handlers are instantiated by the test DI container or used via reflection in tests.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Tests.Notifications.OrderCreatedNotificationHandler"
)]
