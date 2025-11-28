namespace Teqniqly.Arbiter.Core
{
    internal static class MessageContextDefaults
    {
        public static MessageContext New() =>
            new(
                Guid.NewGuid().ToString("N"),
                null,
                null,
                null,
                null,
                new Dictionary<string, object?>()
            );
    }
}
