using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Config
{
    public static class SwaggerStartupConfigRunner
    {
        public static void ConfigureSwaggerGen(SwaggerGenOptions c) {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasktower.BoardService", Version = "v1" });
            var openidScheme = new OpenApiSecurityScheme()
            {
                Name = "openid",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(openidScheme.Reference.Id, openidScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {openidScheme, Array.Empty<string>()}
                });
        }

        public static void ConfigureSwaggerUI(SwaggerUIOptions c)
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasktower.BoardService v1");
        }
    }
}
