namespace Teqniqly.Arbiter.Core.Abstractions
{
    /// <summary>
    /// Mediator contract for sending commands, asking queries and publishing notifications.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Send a query and receive a <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the query result.</typeparam>
        /// <param name="query">The query instance.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> that completes with the query result.</returns>
        ValueTask<TResult> Ask<TResult>(IQuery<TResult> query, CancellationToken ct = default);

        /// <summary>
        /// Publish a notification to all registered handlers.
        /// </summary>
        /// <typeparam name="TNotification">The notification type.</typeparam>
        /// <param name="notification">The notification instance.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="ValueTask"/> that completes when all handlers have executed.</returns>
        ValueTask Publish<TNotification>(TNotification notification, CancellationToken ct = default)
            where TNotification : INotification;

        /// <summary>
        /// Send a command and receive a <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the command result.</typeparam>
        /// <param name="command">The command instance.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> that completes with the command result.</returns>
        ValueTask<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct = default);
    }
}
