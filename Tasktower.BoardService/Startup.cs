using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tasktower.BoardService.Errors.Middleware.Extensions;
using Tasktower.BoardService.Security.Extensions;
using Tasktower.BoardService.Errors.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Tasktower.BoardService.DataAccess.Context;
using Tasktower.BoardService.Security;
using Tasktower.BoardService.Tools.DependencyInjection.Extensions;

namespace Tasktower.BoardService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = HeaderAuthenticationOptions.DefaultScheme;
                    options.DefaultChallengeScheme = HeaderAuthenticationOptions.DefaultScheme;
                })
                .AddHeaderAuthentication(options => { });
            
            services.AddAuthorization(options =>
            {
                var policies = typeof(Policies).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(Policy))
                    .Select(f => (Policy)f.GetValue(null));
                foreach (var policy in policies)
                {
                    options.AddPolicy(policy);
                }
            });
            
            services.AddHttpContextAccessor();
            
            services.AddDbContext<BoardDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLServerBoardDB"));
            });

            // Custom scoped services
            services.AddScopedServices();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasktower.BoardService", Version = "3.0.0" });
            var useridScheme = new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Name = SecurityHeaderNames.UserId,
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
                Name = SecurityHeaderNames.Name,
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
                Name = SecurityHeaderNames.Email,
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
                Name = SecurityHeaderNames.Roles,
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
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasktower.BoardService v1");
                });
            }

            app.UseCustonErrorHandler(new ErrorHandleMiddlewareOptions
            {
                ShowAllErrorMessages = env.IsDevelopment(),
                UseStackTrace = !env.IsProduction()
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
