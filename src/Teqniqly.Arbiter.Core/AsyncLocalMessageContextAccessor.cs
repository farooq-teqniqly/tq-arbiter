using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core
{
    internal sealed class AsyncLocalMessageContextAccessor : IMessageContextAccessor
    {
        private static readonly AsyncLocal<MessageContext?> _current = new();
        public MessageContext? Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }
    }
}
