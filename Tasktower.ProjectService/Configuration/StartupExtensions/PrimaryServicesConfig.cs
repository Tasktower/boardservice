using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.Services;
using Tasktower.ProjectService.Services.External;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class PrimaryServicesConfig
    {
        public static void ConfigurePrimaryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IErrorService, ErrorService>();
            services.AddSingleton<IExternalUserService, ExternalUserService>();
            services.AddScoped<IProjectAuthorizeService, ProjectAuthorizeService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddSingleton<IValidationService, ValidationService>();
        }
    }
}