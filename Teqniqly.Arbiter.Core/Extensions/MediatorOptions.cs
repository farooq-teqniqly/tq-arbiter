using Microsoft.Extensions.DependencyInjection;

namespace Teqniqly.Arbiter.Core.Extensions
{
    /// <summary>
    /// Configuration options that control how mediator handlers are discovered and registered.
    /// </summary>
    /// <remarks>
    /// Configure an instance of this class when registering the mediator services to
    /// influence handler lifetime, uniqueness validation, and which types are considered
    /// during scanning/registration.
    /// </remarks>
    public sealed class MediatorOptions
    {
        /// <summary>
        /// The <see cref="ServiceLifetime"/> used when registering discovered handlers.
        /// </summary>
        /// <value>
        /// Defaults to <see cref="ServiceLifetime.Scoped"/>.
        /// </value>
        public ServiceLifetime HandlerLifetime { get; set; } = ServiceLifetime.Scoped;

        /// <summary>
        /// Optional predicate applied to types discovered during handler scanning.
        /// Only types for which this delegate returns <c>true</c> will be considered
        /// for registration. If <c>null</c>, no additional filtering is applied.
        /// </summary>
        /// <example>
        /// // Example: include only public, non-abstract classes
        /// // options.TypeFilter = t => t.IsClass && !t.IsAbstract && t.IsPublic;
        /// </example>
        public Func<Type, bool>? TypeFilter { get; set; }
    }
}
