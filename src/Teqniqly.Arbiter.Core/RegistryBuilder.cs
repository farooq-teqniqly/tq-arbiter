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

            foreach (var type in src.SelectMany(a => a.DefinedTypes))
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
