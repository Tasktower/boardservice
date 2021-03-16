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

namespace Tasktower.BoardService.Options
{
    public static class SwaggerStartupOptionsRunner
    {
        public static void ConfigureSwaggerGen(SwaggerGenOptions c) {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasktower.BoardService", Version = "3.0.0" });
            //var openidScheme = new OpenApiSecurityScheme()
            //{
            //    Name = "openid",
            //    Description = "Enter JWT Bearer token **_only_**",
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.Http,
            //    Scheme = "bearer", // must be lower case
            //    BearerFormat = "JWT",
            //    Reference = new OpenApiReference
            //    {
            //        Id = JwtBearerDefaults.AuthenticationScheme,
            //        Type = ReferenceType.SecurityScheme
            //    }
            //};
            //c.AddSecurityDefinition(openidScheme.Reference.Id, openidScheme);
            //c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {openidScheme, Array.Empty<string>()}
            //    });
            var useridScheme = new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = "userid",
                Reference = new OpenApiReference
                {
                    Id = "userid header",
                    Type = ReferenceType.SecurityScheme
                }

            };
            c.AddSecurityDefinition(useridScheme.Reference.Id, useridScheme);

            var nameScheme = new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = "name",
                Reference = new OpenApiReference
                {
                    Id = "name header",
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(nameScheme.Reference.Id, nameScheme);
            var emailScheme = new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = "email",
                Reference = new OpenApiReference
                {
                    Id = "email header",
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(emailScheme.Reference.Id, emailScheme);
            var rolesScheme = new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = "roles",
                Reference = new OpenApiReference
                {
                    Id = "roles header",
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(rolesScheme.Reference.Id, rolesScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {useridScheme, Array.Empty<string>()},
                    {nameScheme, Array.Empty<string>()},
                    {emailScheme, Array.Empty<string>()},
                    {rolesScheme, Array.Empty<string>()}
                });
        }

        public static void ConfigureSwaggerUI(SwaggerUIOptions c)
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasktower.BoardService v1");
        }
    }
}
