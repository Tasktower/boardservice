using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tasktower.ProjectService.Configuration.StartupExtensions;
using Tasktower.ProjectService.Errors.Middleware;

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
            services.ConfigureSecurity(Configuration);
            services.ConfigureHttpContext(Configuration);
            services.ConfigureDataMapper(Configuration);
            services.ConfigureDatabaseConnection(Configuration);
            services.ConfigureRepositories(Configuration);
            services.ConfigurePrimaryServices(Configuration);
            services.ConfigureOptionsServices(Configuration);
            services.ConfigureControllers(Configuration);
            services.ConfigureSwagger(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ConfigureSwagger(env);
            }

            app.UseMiddleware<ErrorHandleMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.ConfigureSecurity(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
