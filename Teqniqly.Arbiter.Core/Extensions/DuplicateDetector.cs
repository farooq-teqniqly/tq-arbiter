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

            PopulateHandlerDictionaries(allTypes, commandDictionary, queryDictionary);

            var duplicates = CollectDuplicates(commandDictionary, queryDictionary);

            ThrowIfDuplicatesFound(duplicates);
        }

        /// <summary>
        /// Collects duplicate handler entries from command and query dictionaries.
        /// </summary>
        /// <param name="commandDictionary">Dictionary of command handlers.</param>
        /// <param name="queryDictionary">Dictionary of query handlers.</param>
        /// <returns>List of formatted duplicate handler messages.</returns>
        private static List<string> CollectDuplicates(
            Dictionary<Type, HashSet<Type>> commandDictionary,
            Dictionary<Type, HashSet<Type>> queryDictionary
        )
        {
            var duplicates = new List<string>();

            duplicates.AddRange(GetDuplicateMessages(commandDictionary, "Command"));
            duplicates.AddRange(GetDuplicateMessages(queryDictionary, "Query"));

            return duplicates;
        }

        /// <summary>
        /// Gets formatted duplicate messages from a handler dictionary.
        /// </summary>
        /// <param name="dictionary">The handler dictionary to check.</param>
        /// <param name="handlerType">The type of handler (e.g., "Command" or "Query").</param>
        /// <returns>Enumerable of formatted duplicate messages.</returns>
        private static IEnumerable<string> GetDuplicateMessages(
            Dictionary<Type, HashSet<Type>> dictionary,
            string handlerType
        )
        {
            return dictionary
                .Where(kv => kv.Value.Count > 1)
                .Select(kv =>
                    $"{handlerType} {kv.Key.FullName}: {string.Join(", ", kv.Value.Select(t => t.FullName))}"
                );
        }

        /// <summary>
        /// Populates the command and query handler dictionaries by scanning types for handler implementations.
        /// </summary>
        /// <param name="types">The types to scan for handler implementations.</param>
        /// <param name="commandDictionary">Dictionary to populate with command handlers.</param>
        /// <param name="queryDictionary">Dictionary to populate with query handlers.</param>
        private static void PopulateHandlerDictionaries(
            IEnumerable<TypeInfo> types,
            Dictionary<Type, HashSet<Type>> commandDictionary,
            Dictionary<Type, HashSet<Type>> queryDictionary
        )
        {
            foreach (var typeInfo in types)
            {
                ProcessTypeInterfaces(typeInfo, commandDictionary, queryDictionary);
            }
        }

        /// <summary>
        /// Processes the interfaces of a type to identify and register command and query handlers.
        /// </summary>
        /// <param name="typeInfo">The type to process.</param>
        /// <param name="commandDictionary">Dictionary to populate with command handlers.</param>
        /// <param name="queryDictionary">Dictionary to populate with query handlers.</param>
        private static void ProcessTypeInterfaces(
            TypeInfo typeInfo,
            Dictionary<Type, HashSet<Type>> commandDictionary,
            Dictionary<Type, HashSet<Type>> queryDictionary
        )
        {
            foreach (var interfaceType in typeInfo.GetInterfaces())
            {
                if (!interfaceType.IsGenericType)
                {
                    continue;
                }

                var genericTypeDefinition = interfaceType.GetGenericTypeDefinition();

                if (genericTypeDefinition == _commandDefinition)
                {
                    TryAddHandlerToDictionary(typeInfo, interfaceType, commandDictionary);
                }
                else if (genericTypeDefinition == _queryDefinition)
                {
                    TryAddHandlerToDictionary(typeInfo, interfaceType, queryDictionary);
                }
            }
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

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if duplicates are found.
        /// </summary>
        /// <param name="duplicates">List of duplicate handler messages.</param>
        private static void ThrowIfDuplicatesFound(List<string> duplicates)
        {
            if (duplicates.Count == 0)
            {
                return;
            }

            var exceptionMessage =
                "Multiple handlers were found for the same message type:\n - "
                + string.Join("\n - ", duplicates);

            throw new InvalidOperationException(exceptionMessage);
        }

        /// <summary>
        /// Attempts to add a handler type to the dictionary if it's a closed generic type.
        /// </summary>
        /// <param name="typeInfo">The handler type to add.</param>
        /// <param name="interfaceType">The handler interface type.</param>
        /// <param name="dictionary">The dictionary to add the handler to.</param>
        private static void TryAddHandlerToDictionary(
            TypeInfo typeInfo,
            Type interfaceType,
            Dictionary<Type, HashSet<Type>> dictionary
        )
        {
            // Only consider CLOSED handlers for duplicate validation
            if (typeInfo.IsGenericTypeDefinition)
            {
                return; // open generic not counted
            }

            var messageType = interfaceType.GetGenericArguments()[0];

            if (!dictionary.TryGetValue(messageType, out var handlerSet))
            {
                handlerSet = [];
                dictionary[messageType] = handlerSet;
            }

            handlerSet.Add(typeInfo.AsType());
        }
    }
}
