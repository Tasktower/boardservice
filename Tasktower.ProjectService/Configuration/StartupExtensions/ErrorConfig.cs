using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.Lib.Aspnetcore.Configuration.StartupExtensions;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class ErrorConfig
    {
        public static void ConfigureErrors(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureErrorsBase(configuration);
        }
        
        public static void UseErrorsHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorsHandlingBase<ErrorCode>(env);
        }
    }
}