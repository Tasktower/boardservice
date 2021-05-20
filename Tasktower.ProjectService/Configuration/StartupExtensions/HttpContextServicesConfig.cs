using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class HttpContextServicesConfig
    {
        public static void ConfigureHttpContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
        }
    }
}