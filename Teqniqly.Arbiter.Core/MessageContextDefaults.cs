namespace Teqniqly.Arbiter.Core
{
    /// <summary>
    /// Default helpers for message context creation.
    /// </summary>
    internal static class MessageContextDefaults
    {
        /// <summary>
        /// Create a new default <see cref="MessageContext"/> with a generated correlation id.
        /// </summary>
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
