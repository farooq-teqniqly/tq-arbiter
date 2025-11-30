namespace Teqniqly.Arbiter.Core.Abstractions
{
    /// <summary>
    /// Marker interface for a command that produces a result of type <typeparamref name="TResult"/>.
    /// Commands represent intent to perform an action that returns a value.
    /// </summary>
    /// <typeparam name="TResult">The result type produced by the command.</typeparam>
    public interface ICommand<TResult> { }
}
