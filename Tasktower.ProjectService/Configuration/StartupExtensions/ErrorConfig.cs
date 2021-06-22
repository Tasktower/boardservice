using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.Lib.Aspnetcore.Configuration.StartupExtensions;
using Tasktower.Lib.Aspnetcore.Configuration.StartupExtensions.HelperExtensions;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class ErrorConfig
    {
        public static void ConfigureErrors(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureErrorsGeneric(configuration);
        }
        
        public static void UseErrorsHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorsHandlingGeneric<ErrorCode>(env);
        }
    }
}