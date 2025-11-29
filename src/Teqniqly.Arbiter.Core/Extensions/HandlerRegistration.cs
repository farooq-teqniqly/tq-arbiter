using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Extensions
{
    /// <summary>
    /// Responsible for discovering and registering handler implementations found in the
    /// provided assemblies into an <see cref="IServiceCollection"/>.
    /// </summary>
    /// <remarks>
    /// This class scans assemblies for implementations of <see cref="ICommandHandler{TCommand,TResult}"/>,
    /// <see cref="IQueryHandler{TQuery,TResult}"/>, and <see cref="INotificationHandler{TNotification}"/> and
    /// registers them with the configured lifetime. It also optionally validates that at most one
    /// command/query handler exists per message type (notifications are allowed to have multiple handlers).
    /// </remarks>
    internal static class HandlerRegistration
    {
        private static readonly Type _commandDefinition = typeof(ICommandHandler<,>);
        private static readonly Type _notificationDefinition = typeof(INotificationHandler<>);
        private static readonly Type _queryDefinition = typeof(IQueryHandler<,>);

        /// <summary>
        /// Scans the specified <paramref name="assemblies"/> for handler implementations and registers them
        /// into the provided <paramref name="services"/> using the options in <paramref name="opts"/>.
        /// </summary>
        /// <param name="services">The DI service collection to register handlers into.</param>
        /// <param name="assemblies">Assemblies to scan for handler implementations.</param>
        /// <param name="opts">Registration options that control filtering, handler lifetime and validation behavior.</param>
        public static void RegisterHandlers(
            IServiceCollection services,
            IEnumerable<Assembly> assemblies,
            MediatorOptions opts
        )
        {
            var allTypes = assemblies
                .SelectMany(GetDefinedTypesSafely)
                .Where(t => t is { IsAbstract: false, IsInterface: false })
                .Where(t => opts.TypeFilter is null || opts.TypeFilter(t))
                .ToArray();

            // Track duplicates for commands/queries (closed types only)
            var cmdByMessage = new Dictionary<Type, List<Type>>();
            var qryByMessage = new Dictionary<Type, List<Type>>();

            foreach (var impl in allTypes)
            {
                var interfaces = impl.ImplementedInterfaces.Where(i => i.IsGenericType).ToArray();
                var implIsOpenGeneric = impl.IsGenericTypeDefinition;

                // Command handlers
                RegisterFor(
                    services,
                    impl,
                    implIsOpenGeneric,
                    interfaces,
                    _commandDefinition,
                    opts.HandlerLifetime,
                    cmdByMessage
                );

                // Query handlers
                RegisterFor(
                    services,
                    impl,
                    implIsOpenGeneric,
                    interfaces,
                    _queryDefinition,
                    opts.HandlerLifetime,
                    qryByMessage
                );

                // Notification handlers (fan-out allowed → no duplicate validation)
                RegisterFor(
                    services,
                    impl,
                    implIsOpenGeneric,
                    interfaces,
                    _notificationDefinition,
                    opts.HandlerLifetime,
                    messageTypeTracker: null
                );
            }

            if (!opts.ValidateHandlerUniqueness)
            {
                return;
            }

            ThrowIfDuplicate(cmdByMessage, "command");
            ThrowIfDuplicate(qryByMessage, "query");
        }

        /// <summary>
        /// Safely retrieves the defined types for an assembly. If some types cannot be loaded,
        /// the successfully loaded types are returned.
        /// </summary>
        /// <param name="a">The assembly to inspect.</param>
        /// <returns>Enumerable of <see cref="TypeInfo"/> for the successfully loaded types.</returns>
        private static IEnumerable<TypeInfo> GetDefinedTypesSafely(Assembly a)
        {
            try
            {
                return a.DefinedTypes;
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t is not null).Select(t => t!.GetTypeInfo());
            }
        }

        /// <summary>
        /// Registers a single implementation type for the specified open handler interface definition.
        /// Supports both open-generic registration (registered once) and closed-generic registration (registered per closed interface).
        /// When <paramref name="messageTypeTracker"/> is provided, the method will also track the implementation per message type
        /// for duplicate detection.
        /// </summary>
        private static void RegisterFor(
            IServiceCollection services,
            Type impl,
            bool implIsOpenGeneric,
            Type[] interfaces,
            Type openInterfaceDef,
            ServiceLifetime lifetime,
            Dictionary<Type, List<Type>>? messageTypeTracker
        )
        {
            var matching = interfaces
                .Where(i => i.GetGenericTypeDefinition() == openInterfaceDef)
                .ToArray();

            if (matching.Length == 0)
            {
                return;
            }

            if (implIsOpenGeneric)
            {
                // OPEN generic: register once (DI will close it when needed)
                services.TryAddEnumerable(new ServiceDescriptor(openInterfaceDef, impl, lifetime));
                return;
            }

            // CLOSED: register each closed interface the implementation supports
            foreach (var closedInterface in matching)
            {
                services.TryAddEnumerable(new ServiceDescriptor(closedInterface, impl, lifetime));

                // Track per message type (argument 0 is the message)
                if (messageTypeTracker is null)
                {
                    continue;
                }

                var msgType = closedInterface.GetGenericArguments()[0];

                if (!messageTypeTracker.TryGetValue(msgType, out var list))
                {
                    messageTypeTracker[msgType] = list = [];
                }

                list.Add(impl);
            }
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> when the provided map contains
        /// more than one implementation for any message type.
        /// </summary>
        /// <param name="map">A map from message type to implementations discovered for that message.</param>
        /// <param name="kind">A short name of the kind of handler (e.g. "command" or "query") used in the exception message.</param>
        private static void ThrowIfDuplicate(Dictionary<Type, List<Type>> map, string kind)
        {
            var duplicates = map.Select(kv => new
                {
                    Msg = kv.Key,
                    Impls = kv.Value.Distinct().ToArray(),
                })
                .Where(x => x.Impls.Length > 1)
                .ToArray();

            if (duplicates.Length == 0)
            {
                return;
            }

            var lines = duplicates.Select(x =>
                $"{x.Msg.FullName}: {string.Join(", ", x.Impls.Select(t => t.FullName))}"
            );

            throw new InvalidOperationException(
                $"Multiple {kind} handlers found for the same message type:{Environment.NewLine} - "
                    + string.Join(Environment.NewLine + " - ", lines)
            );
        }
    }
}
