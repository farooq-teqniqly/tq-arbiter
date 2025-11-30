using System.Diagnostics.CodeAnalysis;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Tests;

/// <summary>
/// Tests to verify that duplicate handler registrations throw exceptions.
/// </summary>
public class DuplicateHandlerRegistrationTests
{
    [Fact]
    public void Build_WithDuplicateCommandHandlers_ThrowsInvalidOperationException()
    {
        // Arrange
        var assembly = typeof(DuplicateCommandHandlersTestAssembly).Assembly;

        // Act
        var exception = Assert.Throws<InvalidOperationException>(
            () => RegistryBuilder.Build(assembly)
        );

        // Assert
        Assert.Contains(
            "DuplicateCommand",
            exception.Message,
            StringComparison.Ordinal
        );

        Assert.Contains(
            "Multiple handlers",
            exception.Message,
            StringComparison.Ordinal
        );
    }

    [Fact]
    public void Build_WithDuplicateQueryHandlers_ThrowsInvalidOperationException()
    {
        // Arrange
        var assembly = typeof(DuplicateQueryHandlersTestAssembly).Assembly;

        // Act
        var exception = Assert.Throws<InvalidOperationException>(
            () => RegistryBuilder.Build(assembly)
        );

        // Assert
        Assert.Contains(
            "DuplicateQuery",
            exception.Message,
            StringComparison.Ordinal
        );

        Assert.Contains(
            "Multiple handlers",
            exception.Message,
            StringComparison.Ordinal
        );
    }
}

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Used by reflection during handler registration"
)]
internal sealed class DuplicateCommandHandler1 : ICommandHandler<DuplicateCommand, string>
{
    public ValueTask<string> Handle(DuplicateCommand command, CancellationToken ct)
    {
        return ValueTask.FromResult(command.Value);
    }
}

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Used by reflection during handler registration"
)]
internal sealed class DuplicateCommandHandler2 : ICommandHandler<DuplicateCommand, string>
{
    public ValueTask<string> Handle(DuplicateCommand command, CancellationToken ct)
    {
        return ValueTask.FromResult(command.Value);
    }
}

/// <summary>
/// Marker type for duplicate command handler test assembly.
/// </summary>
[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Marker type for test assembly"
)]
internal sealed class DuplicateCommandHandlersTestAssembly { }

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Used by reflection during handler registration"
)]
internal sealed class DuplicateQueryHandler1 : IQueryHandler<DuplicateQuery, int>
{
    public ValueTask<int> Handle(DuplicateQuery query, CancellationToken ct)
    {
        return ValueTask.FromResult(query.Id);
    }
}

/// <summary>
/// Marker type for duplicate query handler test assembly.
/// </summary>
[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Marker type for test assembly"
)]
internal sealed class DuplicateQueryHandlersTestAssembly { }

// Test types for duplicate command detection
internal sealed record DuplicateCommand(string Value) : ICommand<string>;

// Test types for duplicate query detection
internal sealed record DuplicateQuery(int Id) : IQuery<int>;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Used by reflection during handler registration"
)]
internal sealed class DuplicateQueryHandler2 : IQueryHandler<DuplicateQuery, int>
{
    public ValueTask<int> Handle(DuplicateQuery query, CancellationToken ct)
    {
        return ValueTask.FromResult(query.Id);
    }
}
