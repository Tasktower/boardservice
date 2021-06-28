using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.BusinessLogic;
using Tasktower.ProjectService.BusinessLogic.External;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class BusinessLogicServicesConfig
    {
        public static void ConfigureBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IErrorService, ErrorService>();
            services.AddSingleton<IExternalUserService, ExternalUserService>();
            services.AddScoped<IUserSyncService, UserSyncService>();
            services.AddScoped<IProjectAuthorizeService, ProjectAuthorizeService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddSingleton<IValidationService, ValidationService>();
        }
    }
}