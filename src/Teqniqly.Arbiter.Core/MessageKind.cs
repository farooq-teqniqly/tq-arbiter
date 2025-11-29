namespace Teqniqly.Arbiter.Core
{
    /// <summary>
    /// Kinds of messages the mediator supports.
    /// </summary>
    internal enum MessageKind
    {
        /// <summary>Command message.</summary>
        Command,

        /// <summary>Query message.</summary>
        Query,
    }
}
