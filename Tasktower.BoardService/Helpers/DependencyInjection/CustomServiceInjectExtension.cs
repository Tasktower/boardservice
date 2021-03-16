using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Helpers.DependencyInjection
{
    public static class CustomServiceInjectExtension
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Union(Assembly.GetCallingAssembly().GetTypes())
                .Distinct()
                .Where(t => t.GetCustomAttribute<ScopedServiceAttribute>() is not null);
            foreach (Type t in types)
            {
                services.AddScoped(t.GetInterface($"I{t.Name}"), t);
            }
            return services;
        }
    }
}