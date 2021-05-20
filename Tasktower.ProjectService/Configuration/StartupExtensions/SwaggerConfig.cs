using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasktower.ProjectService", Version = "3.0.0" });
                
                var bearerSchema = new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",  
                    
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer Token",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(bearerSchema.Reference.Id, bearerSchema);
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {bearerSchema, Array.Empty<string>()}
                });
            });
        }
        
        public static void ConfigureSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                    "Tasktower.ProjectService Local v1");
                c.SwaggerEndpoint("/api/projectservice/swagger/v1/swagger.json", 
                    "Tasktower.ProjectService Deploy v1");
            });
        }
    }
}