using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.Configuration.Options;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class OptionsServicesConfig
    {
        public static void ConfigureOptionsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ErrorOptionsConfig>(configuration.GetSection("Errors"));
        }
    }
}