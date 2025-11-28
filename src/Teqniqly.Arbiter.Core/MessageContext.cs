namespace Teqniqly.Arbiter.Core
{
    public sealed record MessageContext(
        string CorrelationId,
        string? CausationId,
        string? TenantId,
        string? UserId,
        string? IdempotencyKey,
        IReadOnlyDictionary<string, object?> Items
    );
}
