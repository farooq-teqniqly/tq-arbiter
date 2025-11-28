using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddArbiter(
            this IServiceCollection services,
            Action<MediatorOptions>? configure = null,
            params Assembly[] assemblies
        )
        {
            var opts = new MediatorOptions();
            configure?.Invoke(opts);

            var registry = RegistryBuilder.Build(assemblies);
            services.AddSingleton(registry);
            services.AddSingleton<IMessageContextAccessor, AsyncLocalMessageContextAccessor>();
            services.AddScoped<IMediator, DefaultMediator>();
            return services;
        }
    }
}
