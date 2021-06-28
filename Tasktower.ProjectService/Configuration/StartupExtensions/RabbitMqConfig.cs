using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;
using Tasktower.Lib.Aspnetcore.RabbitMQ;
using Tasktower.ProjectService.MessageHandlers;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class RabbitMqConfig
    {
        public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var dataUpdateExchangeConf = configuration.GetSection("RabbitMqExchanges:DataUpdate");
            services.AddRabbitMqClient(dataUpdateExchangeConf)
                .AddConsumptionExchange(ExchangeDataUpdateUtils.ExchangeName,
                    configuration.GetSection("RabbitMqExchanges:DataUpdate"))
                .AddMessageHandlerSingleton<UserUpdateMessageHandler>(
                    $"{ExchangeDataUpdateUtils.Models.User}.*", 
                    ExchangeDataUpdateUtils.ExchangeName);
        }
    }
}