using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.Lib.Aspnetcore.Configuration.StartupExtensions.HelperExtensions;
using Tasktower.ProjectService.Configuration.Options;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class OptionsServicesConfig
    {
        public static void ConfigureOptionsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptionConfig<ExternalServicesOptionsConfig>(configuration);
        }
    }
}