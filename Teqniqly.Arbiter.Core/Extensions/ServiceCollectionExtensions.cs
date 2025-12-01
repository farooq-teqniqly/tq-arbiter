using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Arbiter mediator services into an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Arbiter mediator infrastructure into the provided <paramref name="services"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to which Arbiter services will be added. This parameter must not be <c>null</c>.
        /// </param>
        /// <param name="configure">
        /// Optional configuration delegate that configures a <see cref="MediatorOptions"/> instance used during registration.
        /// If <c>null</c>, default options are used.
        /// </param>
        /// <param name="scanAssemblies">
        /// Optional assemblies to scan for handler implementations.
        /// - If one or more assemblies are provided, those assemblies are scanned (duplicates are ignored).
        /// - If none are provided, the calling assembly and the entry assembly are used (any <c>null</c> entries are ignored).
        /// </param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance so calls can be chained.
        /// </returns>
        /// <remarks>
        /// Registration details:
        /// - A handler registry built by <c>RegistryBuilder.Build</c> is registered as a singleton.
        /// - <see cref="IMessageContextAccessor"/> is registered as a singleton (implementation: <c>AsyncLocalMessageContextAccessor</c>).
        /// - <see cref="IMediator"/> is registered as scoped (implementation: <c>DefaultMediator</c>).
        /// - Handlers discovered via scanning are auto-registered using <c>HandlerRegistration.RegisterHandlers</c> with the lifetime specified in <see cref="MediatorOptions.HandlerLifetime"/>.
        ///
        /// Notes:
        /// - The method performs no explicit null-check for <paramref name="services"/> because it is an extension method; callers must ensure it is not <c>null</c>.
        /// </remarks>
        public static IServiceCollection AddArbiter(
            this IServiceCollection services,
            Action<MediatorOptions>? configure = null,
            params Assembly[] scanAssemblies
        )
        {
            var opts = new MediatorOptions();
            configure?.Invoke(opts);

            // Determine assemblies to scan (defaults to calling + entry)
            var assemblies =
                (scanAssemblies?.Length ?? 0) > 0
                    ? scanAssemblies!.Distinct().ToArray()
                    : new[] { Assembly.GetCallingAssembly(), Assembly.GetEntryAssembly() }
                        .Where(a => a is not null)
                        .Distinct()
                        .ToArray();

            // Auto-register handlers
            HandlerRegistration.RegisterHandlers(services, assemblies!, opts);

            //  Build immutable runtime registry (fast dispatch, validates no duplicates)
            var registry = RegistryBuilder.Build(assemblies!);

            // Register core services
            services.AddSingleton(registry);
            services.AddSingleton<IMessageContextAccessor, AsyncLocalMessageContextAccessor>();
            services.AddScoped<IMediator, DefaultMediator>();

            return services;
        }

        /// <summary>
        /// Registers Arbiter mediator infrastructure into the provided <paramref name="services"/>
        /// scanning only the provided <paramref name="scanAssemblies"/> and using the default <see cref="MediatorOptions"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which Arbiter services will be added.</param>
        /// <param name="scanAssemblies">Assemblies to scan for handler implementations.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance so calls can be chained.</returns>
        public static IServiceCollection AddArbiter(
            this IServiceCollection services,
            params Assembly[] scanAssemblies
        ) => AddArbiter(services, configure: null, scanAssemblies);
    }
}
