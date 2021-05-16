using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tasktower.ProjectService.Errors.Middleware.Extensions;
using Tasktower.ProjectService.Security.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Tasktower.ProjectService.Tools.DependencyInjection.Extensions;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.Errors.Middleware;
using Tasktower.ProjectService.Security;

namespace Tasktower.ProjectService
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
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o => {
                o.Authority = Configuration["Auth:Authority"];
                o.Audience = Configuration["Auth:Audience"];
            });
            
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                        "Tasktower.ProjectService Local v1");
                    c.SwaggerEndpoint("/api/projectservice/swagger/v1/swagger.json", 
                        "Tasktower.ProjectService Deploy v1");
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
