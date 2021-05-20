using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class DataMapperServicesConfig
    {
        public static void ConfigureDataMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}