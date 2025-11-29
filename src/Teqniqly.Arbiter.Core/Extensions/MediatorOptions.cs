namespace Teqniqly.Arbiter.Core.Extensions
{
    /// <summary>
    /// Configuration options that control runtime behavior of the mediator.
    /// </summary>
    /// <remarks>
    /// Use this type to configure how the mediator reacts to missing or ambiguous handlers:
    /// - When <see cref="ThrowOnMissingHandler"/> is <c>true</c> the mediator will throw
    ///   when a request has no registered handler.
    /// - When <see cref="ValidateHandlerUniqueness"/> is <c>true</c> the mediator will
    ///   validate at registration or startup that exactly one handler exists per request type
    ///   and will surface an error if multiple handlers are found.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Disable throwing when no handler is found and skip uniqueness validation:
    /// var options = new MediatorOptions
    /// {
    ///     ThrowOnMissingHandler = false,
    ///     ValidateHandlerUniqueness = false
    /// };
    /// </code>
    /// </example>
    public sealed class MediatorOptions
    {
        /// <summary>
        /// When <c>true</c> the mediator will throw an exception if a request is dispatched
        /// and no handler is found for that request type.
        /// </summary>
        /// <value>The default value is <c>true</c>.</value>
        public bool ThrowOnMissingHandler { get; init; } = true;

        /// <summary>
        /// When <c>true</c> the mediator will validate that only one handler exists for each
        /// request type. If multiple handlers are discovered, the mediator should surface an error
        /// during registration/startup.
        /// </summary>
        /// <value>The default value is <c>true</c>.</value>
        public bool ValidateHandlerUniqueness { get; init; } = true;
    }
}
