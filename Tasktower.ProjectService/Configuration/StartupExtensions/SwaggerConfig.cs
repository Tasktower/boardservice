using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Tasktower.Lib.Aspnetcore.Configuration.StartupExtensions;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSwaggerBase( configuration,"Tasktower.ProjectService");
        }
        
        public static void ConfigureSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSwaggerBase(env, "Tasktower.ProjectService");
        }
    }
}