// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "SonarAnalyzer.Rules",
    "S2326:Unused type parameters",
    Justification = "Marker generic interfaces where TResult is intentionally unused.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Abstractions.ICommand`1"
)]

[assembly: SuppressMessage(
    "SonarAnalyzer.Rules",
    "S2326:Unused type parameters",
    Justification = "Marker generic interfaces where TResult is intentionally unused.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Abstractions.INotification`1"
)]

[assembly: SuppressMessage(
    "SonarAnalyzer.Rules",
    "S2326:Unused type parameters",
    Justification = "Marker generic interfaces where TResult is intentionally unused.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Abstractions.IQuery`1"
)]

[assembly: SuppressMessage(
    "Microsoft.Design",
    "CA1040:AvoidEmptyInterfaces",
    Justification = "These are marker interfaces used for intent and type discrimination.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Abstractions.ICommand`1"
)]
[assembly: SuppressMessage(
    "Microsoft.Design",
    "CA1040:AvoidEmptyInterfaces",
    Justification = "These are marker interfaces used for intent and type discrimination.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Abstractions.INotification"
)]

[assembly: SuppressMessage(
    "Microsoft.Design",
    "CA1040:AvoidEmptyInterfaces",
    Justification = "These are marker interfaces used for intent and type discrimination.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.Abstractions.IQuery`1"
)]

[assembly: SuppressMessage(
    "Microsoft.Performance",
    "CA1812:AvoidUninstantiatedInternalClasses",
    Justification = "Instantiated via DI (ServiceProvider) at runtime; reported as false positive by analyzer.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.DefaultMediator"
)]

[assembly: SuppressMessage(
    "Microsoft.Performance",
    "CA1812:AvoidUninstantiatedInternalClasses",
    Justification = "Instantiated via DI (ServiceProvider) at runtime; reported as false positive by analyzer.",
    Scope = "type",
    Target = "~T:Teqniqly.Arbiter.Core.AsyncLocalMessageContextAccessor"
)]
