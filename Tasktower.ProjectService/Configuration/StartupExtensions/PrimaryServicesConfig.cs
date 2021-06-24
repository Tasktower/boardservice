using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.Services;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class PrimaryServicesConfig
    {
        public static void ConfigurePrimaryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IErrorService, ErrorService>();
            services.AddScoped<IProjectAuthorizeService, ProjectAuthorizeService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddSingleton<IValidationService, ValidationService>();
        }
    }
}