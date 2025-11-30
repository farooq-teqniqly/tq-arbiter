using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core
{
    /// <summary>
    /// Accessor for the ambient <see cref="MessageContext"/> implemented using
    /// <see cref="AsyncLocal{T}"/> so the value is scoped to the executing async control flow.
    /// </summary>
    /// <remarks>
    /// The value stored by this accessor is local to the current asynchronous/thread context and
    /// flows with async/await. Assigning to <see cref="Current"/> replaces the context for the
    /// current flow only; other threads or async flows will observe their own values.
    ///
    /// This type is internal and sealed because it is an implementation detail for the
    /// <see cref="IMessageContextAccessor"/> abstraction.
    ///
    /// To avoid leaking context across unrelated operations, clear the context (set to <c>null</c>)
    /// when it is no longer required.
    /// </remarks>
    internal sealed class AsyncLocalMessageContextAccessor : IMessageContextAccessor
    {
        /// <summary>
        /// Backing storage for the ambient <see cref="MessageContext"/>. Implemented with
        /// <see cref="AsyncLocal{T}"/> so the value flows with the async control flow.
        /// </summary>
        private static readonly AsyncLocal<MessageContext?> _current = new();

        /// <summary>
        /// Gets or sets the current <see cref="MessageContext"/> for the executing async flow,
        /// or <c>null</c> when none is set.
        /// </summary>
        /// <value>The ambient <see cref="MessageContext"/> for the current async flow, or <c>null</c>.</value>
        /// <remarks>
        /// Use this property to access or replace the ambient message context. Setting this property
        /// affects only the current async/thread flow. Clear the value (assign <c>null</c>) when finished
        /// to prevent accidental context leakage.
        /// </remarks>
        public MessageContext? Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }
    }
}
