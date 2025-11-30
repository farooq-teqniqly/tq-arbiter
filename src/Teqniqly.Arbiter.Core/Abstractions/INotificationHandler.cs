namespace Teqniqly.Arbiter.Core.Abstractions
{
    /// <summary>
    /// Handler for notifications of type <typeparamref name="TNotification"/>.
    /// </summary>
    /// <typeparam name="TNotification">The notification type.</typeparam>
    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        /// <summary>
        /// Handle the specified notification.
        /// </summary>
        /// <param name="notification">The notification instance.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="ValueTask"/> that completes when handling is finished.</returns>
        ValueTask Handle(TNotification notification, CancellationToken ct);
    }
}
