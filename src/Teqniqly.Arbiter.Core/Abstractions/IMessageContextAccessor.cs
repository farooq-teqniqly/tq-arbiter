namespace Teqniqly.Arbiter.Core.Abstractions
{
    public interface IMessageContextAccessor
    {
        MessageContext? Current { get; set; }
    }
}
