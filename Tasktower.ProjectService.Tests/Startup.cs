
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tasktower.Lib.Aspnetcore.Configuration.StartupExtensions;
using Tasktower.ProjectService.Configuration.StartupExtensions;
using Tasktower.ProjectService.Tests.TestTools.DependencyInjection;

namespace Tasktower.ProjectService.Tests
{
    public class Startup
    {
        public ILoggerFactory LoggerFactory { get; internal set; }
        public IConfiguration Configuration { get; internal set; }
        
        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder
                .ConfigureHostConfiguration(builder => SetupConfiguration(builder))
                .ConfigureLogging(builder => { })
                .ConfigureServices(ConfigureServices)
                .ConfigureAppConfiguration((context, builder) => {});
        
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = SetupConfiguration().Build();
            services.ConfigureErrors(Configuration);
            services.ConfigureHttpContext(Configuration);
            services.ConfigureDataMapper(Configuration);
            services.ConfigureBusinessLogicServices(Configuration);
            services.ConfigureOptionsServices(Configuration);
            services.ConfigureInMemoryDatabase(Configuration);
            services.ConfigureRepositories(Configuration);
            services.ConfigureHttpClient(Configuration);
        }

        private static IConfigurationBuilder SetupConfiguration()
        {
            return SetupConfiguration(new ConfigurationBuilder());
        }
        private static IConfigurationBuilder SetupConfiguration(IConfigurationBuilder builder)
        {
            return builder
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false);
        }
    }
}