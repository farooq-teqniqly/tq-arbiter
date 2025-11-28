using System.Reflection;
using Teqniqly.Arbiter.Core.Abstractions;

namespace Teqniqly.Arbiter.Core
{
    internal static class RegistryBuilder
    {
        public static HandlerRegistry Build(params Assembly[] assemblies)
        {
            var reg = new HandlerRegistry();
            var src = assemblies is { Length: > 0 } ? assemblies : [Assembly.GetCallingAssembly()];

            foreach (var type in src.SelectMany(a => a.DefinedTypes))
            {
                foreach (var itf in type.ImplementedInterfaces)
                {
                    if (!itf.IsGenericType)
                    {
                        continue;
                    }

                    var tdef = itf.GetGenericTypeDefinition();
                    var args = itf.GetGenericArguments();

                    if (tdef == typeof(ICommandHandler<,>))
                    {
                        typeof(HandlerRegistry)
                            .GetMethod(nameof(HandlerRegistry.AddCommand))!
                            .MakeGenericMethod(args[0], args[1])
                            .Invoke(reg, null);
                    }
                    else if (tdef == typeof(IQueryHandler<,>))
                    {
                        typeof(HandlerRegistry)
                            .GetMethod(nameof(HandlerRegistry.AddQuery))!
                            .MakeGenericMethod(args[0], args[1])
                            .Invoke(reg, null);
                    }
                    else if (tdef == typeof(INotificationHandler<>))
                    {
                        typeof(HandlerRegistry)
                            .GetMethod(nameof(HandlerRegistry.AddNotification))!
                            .MakeGenericMethod(args[0])
                            .Invoke(reg, null);
                    }
                }
            }
            return reg;
        }
    }
}
