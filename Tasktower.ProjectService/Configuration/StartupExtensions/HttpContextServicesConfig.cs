using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.Security;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class HttpContextServicesConfig
    {
        public static void ConfigureHttpContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}