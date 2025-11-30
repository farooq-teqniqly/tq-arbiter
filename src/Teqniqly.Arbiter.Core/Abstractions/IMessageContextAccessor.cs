namespace Teqniqly.Arbiter.Core.Abstractions
{
    /// <summary>
    /// Accessor for the ambient <see cref="MessageContext"/>, implemented using <see cref="AsyncLocal{T}"/>.
    /// </summary>
    public interface IMessageContextAccessor
    {
        /// <summary>
        /// The current message context for the executing async flow, or <c>null</c> when none is set.
        /// </summary>
        MessageContext? Current { get; set; }
    }
}
