using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.Services;
using Tasktower.ProjectService.Services.Impl;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class PrimaryServicesConfig
    {
        public static void ConfigurePrimaryServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSingleton<IErrorService, ErrorService>();
            services.AddScoped<IProjectAuthorizeService, ProjectAuthorizeService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IUserContextService, UserContextService>();
        }
    }
}