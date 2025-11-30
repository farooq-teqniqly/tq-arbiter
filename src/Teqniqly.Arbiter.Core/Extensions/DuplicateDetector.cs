using System.Reflection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core.Extensions
{
    /// <summary>
    /// Detects duplicate command and query handlers across a set of assemblies.
    /// When more than one handler implementation is found for the same message type,
    /// <see cref="ThrowIfDuplicates"/> will throw an <see cref="InvalidOperationException"/> describing the conflicts.
    /// </summary>
    internal static class DuplicateDetector
    {
        private static readonly Type _commandDefinition = typeof(ICommandHandler<,>);
        private static readonly Type _queryDefinition = typeof(IQueryHandler<,>);

        /// <summary>
        /// Scans the provided <paramref name="assemblies"/> for implementations of
        /// <see cref="ICommandHandler{TCommand,TResult}"/> and <see cref="IQueryHandler{TQuery,TResult}"/>.
        /// If more than one concrete handler is found for the same message type, an
        /// <see cref="InvalidOperationException"/> is thrown listing the duplicate handlers.
        /// </summary>
        /// <param name="assemblies">The assemblies to scan for handler implementations. If an assembly partially fails to load types,
        /// successfully loaded types will still be scanned.</param>
        public static void ThrowIfDuplicates(IEnumerable<Assembly> assemblies)
        {
            var allTypes = assemblies
                .SelectMany(SafeTypes)
                .Where(t => t is { IsAbstract: false, IsInterface: false });

            var commandDictionary = new Dictionary<Type, HashSet<Type>>();
            var queryDictionary = new Dictionary<Type, HashSet<Type>>();

            foreach (var typeInfo in allTypes)
            {
                foreach (var interfaceType in typeInfo.GetInterfaces())
                {
                    if (!interfaceType.IsGenericType)
                    {
                        continue;
                    }

                    var genericTypeDefinition = interfaceType.GetGenericTypeDefinition();

                    // Only consider CLOSED handlers for duplicate validation
                    if (genericTypeDefinition == _commandDefinition)
                    {
                        var argType = interfaceType.GetGenericArguments()[0];

                        if (typeInfo.IsGenericTypeDefinition)
                        {
                            continue; // open generic not counted
                        }

                        if (!commandDictionary.TryGetValue(argType, out var set))
                        {
                            set = [];
                            commandDictionary[argType] = set;
                        }

                        set.Add(typeInfo.AsType());
                    }
                    else if (genericTypeDefinition == _queryDefinition)
                    {
                        var argType = interfaceType.GetGenericArguments()[0];

                        if (typeInfo.IsGenericTypeDefinition)
                        {
                            continue;
                        }

                        if (!queryDictionary.TryGetValue(argType, out var set))
                        {
                            set = [];
                            queryDictionary[argType] = set;
                        }

                        set.Add(typeInfo.AsType());
                    }
                }
            }

            var duplicates = new List<string>();

            duplicates.AddRange(
                commandDictionary
                    .Where(kv => kv.Value.Count > 1)
                    .Select(kv =>
                        $"Command {kv.Key.FullName}: {string.Join(", ", kv.Value.Select(t => t.FullName))}"
                    )
            );

            duplicates.AddRange(
                queryDictionary
                    .Where(kv => kv.Value.Count > 1)
                    .Select(kv =>
                        $"Query   {kv.Key.FullName}: {string.Join(", ", kv.Value.Select(t => t.FullName))}"
                    )
            );

            if (duplicates.Count <= 0)
            {
                return;
            }

            var exceptionMessage =
                "Multiple handlers were found for the same message type:\n - "
                + string.Join("\n - ", duplicates);

            throw new InvalidOperationException(exceptionMessage);
        }

        /// <summary>
        /// Returns the defined types for an assembly. If the assembly fails to load some types,
        /// the successfully loaded types are returned and load exceptions are ignored.
        /// </summary>
        /// <param name="a">The assembly to inspect.</param>
        /// <returns>Enumerable of <see cref="TypeInfo"/> for the successfully loaded types.</returns>
        private static IEnumerable<TypeInfo> SafeTypes(Assembly a)
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
    }
}
