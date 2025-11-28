namespace Teqniqly.Arbiter.Core.Extensions
{
    public sealed class MediatorOptions
    {
        public bool ThrowOnMissingHandler { get; init; } = true;
        public bool ValidateHandlerUniqueness { get; init; } = true;
    }
}
