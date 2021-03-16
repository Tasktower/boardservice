using System;
using System.Collections.Generic;
using System.Linq;
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
using Tasktower.BoardService.Options;
using Tasktower.BoardService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Tasktower.BoardService.Helpers.DependencyInjection;

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
            services.AddAuthentication(AuthStartupOptionsRunner.ConfigureAuthentication)
                .AddHeaderAuthentication(options => { });
            services.AddAuthorization(AuthStartupOptionsRunner.ConfigureAuthorization);
            services.AddHttpContextAccessor();
            services.AddDbContext<BoardDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLServerBoardDB"));
            });

            // Custom scoped services
            services.AddScopedServices();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(SwaggerStartupOptionsRunner.ConfigureSwaggerGen);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(SwaggerStartupOptionsRunner.ConfigureSwaggerUI);
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
