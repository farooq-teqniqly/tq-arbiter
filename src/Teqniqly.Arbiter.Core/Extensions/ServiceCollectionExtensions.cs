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
        /// Registers the Arbiter mediator and related infrastructure into the provided <paramref name="services"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add Arbiter services to. This parameter must not be <c>null</c>.
        /// </param>
        /// <param name="assemblies">
        /// Optional assemblies to scan when building the internal handler registry. Pass one or more assemblies that contain
        /// handler implementations. If no assemblies are provided an empty array is forwarded to <c>RegistryBuilder.Build</c>.
        /// </param>
        /// <returns>
        /// Returns the same <see cref="IServiceCollection"/> instance so that calls can be chained.
        /// </returns>
        /// <remarks>
        /// The following services are registered:
        /// - The handler registry returned by <c>RegistryBuilder.Build</c> is registered as a singleton.
        /// - <see cref="IMessageContextAccessor"/> is registered as a singleton (implementation: <c>AsyncLocalMessageContextAccessor</c>).
        /// - <see cref="IMediator"/> is registered as scoped (implementation: <c>DefaultMediator</c>).
        /// </remarks>
        public static IServiceCollection AddArbiter(
            this IServiceCollection services,
            params Assembly[] assemblies
        )
        {
            var registry = RegistryBuilder.Build(assemblies);
            services.AddSingleton(registry);
            services.AddSingleton<IMessageContextAccessor, AsyncLocalMessageContextAccessor>();
            services.AddScoped<IMediator, DefaultMediator>();
            return services;
        }
    }
}
