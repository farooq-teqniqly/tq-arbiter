namespace Teqniqly.Arbiter.Core
{
    /// <summary>
    /// Context information propagated with messages through the mediator.
    /// </summary>
    /// <param name="CorrelationId">Correlation identifier for the message flow.</param>
    /// <param name="CausationId">Optional causation identifier.</param>
    /// <param name="TenantId">Optional tenant identifier.</param>
    /// <param name="UserId">Optional user identifier.</param>
    /// <param name="IdempotencyKey">Optional idempotency key.</param>
    /// <param name="Items">Optional bag of additional items.</param>
    public sealed record MessageContext(
        string CorrelationId,
        string? CausationId,
        string? TenantId,
        string? UserId,
        string? IdempotencyKey,
        IReadOnlyDictionary<string, object?> Items
    );
}
