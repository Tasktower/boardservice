using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tasktower.Lib.Aspnetcore.Configuration.StartupExtensions;
using Tasktower.ProjectService.Configuration.StartupExtensions;

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
            services.ConfigureErrors(Configuration);
            services.ConfigureAuth(Configuration);
            services.ConfigureRabbitMq(Configuration);
            services.ConfigureHttpClient(Configuration);
            services.ConfigureHttpContext(Configuration);
            services.ConfigureDataMapper(Configuration);
            services.ConfigureDatabaseConnection(Configuration);
            services.ConfigureRepositories(Configuration);
            services.ConfigureBusinessLogicServices(Configuration);
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
                app.ConfigureSwagger(env, Configuration);
            } 
            else if (env.IsStaging())
            {
                app.ConfigureSwagger(env, Configuration);
            }

            app.UseErrorsHandling(env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuth(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
