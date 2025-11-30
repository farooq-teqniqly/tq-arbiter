namespace Teqniqly.Arbiter.Core
{
    /// <summary>
    /// Internal key used by the handler registry to identify a handler by message kind and type.
    /// </summary>
    internal readonly record struct RegistryKey(MessageKind Kind, Type MessageType);
}
