namespace Teqniqly.Arbiter.Core.Abstractions
{
    /// <summary>
    /// Handler for a command of type <typeparamref name="TCommand"/> that returns <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <param name="command">The command instance.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The result produced by handling the command.</returns>
        ValueTask<TResult> Handle(TCommand command, CancellationToken ct);
    }
}
