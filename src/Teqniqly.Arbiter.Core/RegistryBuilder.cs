using System.Reflection;
using Teqniqly.Arbiter.Core.Abstractions;
using Teqniqly.Arbiter.Core.Extensions;

namespace Teqniqly.Arbiter.Core
{
    /// <summary>
    /// Scans assemblies for handler implementations and populates a <see cref="HandlerRegistry"/>.
    /// </summary>
    internal static class RegistryBuilder
    {
        /// <summary>
        /// Build a handler registry by scanning the provided assemblies (or the calling assembly when none provided).
        /// </summary>
        /// <param name="assemblies">Assemblies to scan for handlers.</param>
        /// <returns>A populated <see cref="HandlerRegistry"/>.</returns>
        public static HandlerRegistry Build(params Assembly[] assemblies)
        {
            var src = assemblies is { Length: > 0 } ? assemblies : [Assembly.GetCallingAssembly()];

            // Validate no duplicate handlers before building registry
            DuplicateDetector.ThrowIfDuplicates(src);

            var reg = new HandlerRegistry();

            foreach (var type in src.SelectMany(GetLoadableTypes))
            {
                foreach (var itf in type.ImplementedInterfaces)
                {
                    if (!itf.IsGenericType)
                    {
                        continue;
                    }

                    var typeDef = itf.GetGenericTypeDefinition();
                    var args = itf.GetGenericArguments();

                    if (typeDef == typeof(ICommandHandler<,>))
                    {
                        RegisterInvoker(nameof(HandlerRegistry.AddCommand), reg, args[0], args[1]);
                    }
                    else if (typeDef == typeof(IQueryHandler<,>))
                    {
                        RegisterInvoker(nameof(HandlerRegistry.AddQuery), reg, args[0], args[1]);
                    }
                    else if (typeDef == typeof(INotificationHandler<>))
                    {
                        RegisterInvoker(nameof(HandlerRegistry.AddNotification), reg, args[0]);
                    }
                }
            }

            return reg;
        }

        /// <summary>
        /// Returns the defined types for an assembly. If the assembly fails to load some types
        /// (typically due to missing dependencies), the successfully loaded types are returned
        /// and load exceptions are ignored.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <returns>Enumerable of <see cref="TypeInfo"/> for the successfully loaded types.</returns>
        private static IEnumerable<TypeInfo> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.DefinedTypes;
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Return only the types that loaded successfully, filtering out nulls
                return ex.Types.Where(t => t is not null).Select(t => t!.GetTypeInfo());
            }
        }

        /// <summary>
        /// Locates a generic registration method on <see cref="HandlerRegistry"/>, closes it with the provided
        /// generic type arguments and invokes it on the given registry instance.
        /// </summary>
        /// <param name="methodName">
        /// Name of the parameterless generic registration method on <see cref="HandlerRegistry"/> to invoke.
        /// Typical values: "AddCommand", "AddQuery", "AddNotification".
        /// </param>
        /// <param name="reg">The <see cref="HandlerRegistry"/> instance to invoke the method on.</param>
        /// <param name="args">
        /// The generic type arguments to apply to the target method. The number and order must match the
        /// generic parameters expected by the target method (for example, 2 types for AddCommand/AddQuery,
        /// 1 type for AddNotification).
        /// </param>
        /// <remarks>
        /// This method uses reflection (<see cref="Type.GetMethod"/>, <see cref="MethodInfo.MakeGenericMethod"/>,
        /// and <see cref="MethodInfo.Invoke"/>) to dynamically construct and call the appropriate registration
        /// method. The invoked registration methods are parameterless; therefore no arguments are passed to the
        /// invoked method.
        ///
        /// Any exceptions thrown by reflection (for example, if the method name does not exist, the generic
        /// arity does not match, or the invoked method throws) will propagate to the caller. This behavior
        /// surfaces registry misconfiguration early during startup.
        /// </remarks>
        private static void RegisterInvoker(
            string methodName,
            HandlerRegistry reg,
            params Type[] args
        )
        {
            typeof(HandlerRegistry)
                .GetMethod(methodName)!
                .MakeGenericMethod(args)
                .Invoke(reg, null);
        }
    }
}
