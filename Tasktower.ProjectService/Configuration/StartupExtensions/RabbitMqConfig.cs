using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Configuration;
using RabbitMQ.Client.Core.DependencyInjection.InternalExtensions;
using Tasktower.Lib.Aspnetcore.RabbitMQ;
using Tasktower.ProjectService.RabbitMq;
using Tasktower.ProjectService.RabbitMq.MessageHandlers;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class RabbitMqConfig
    {
        public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqClientConf = configuration.GetSection("RabbitMq");
            var dataUpdateExchangeConf = configuration.GetSection("RabbitMqExchanges:DataUpdate");
            
            services.AddRabbitMqClient(rabbitMqClientConf)
                .AddRabbitMqConsumingClientSingleton(configuration.GetSection("RabbitMq"))
                .AddConsumptionExchange(ExchangeDataUpdateUtils.ExchangeName, dataUpdateExchangeConf)
                .AddAsyncMessageHandlerSingleton<UserUpdateMessageHandler>(
                    $"{ExchangeDataUpdateUtils.Models.User}.{ExchangeDataUpdateUtils.Actions.Update}", 
                    ExchangeDataUpdateUtils.ExchangeName)
                .AddAsyncMessageHandlerSingleton<UserDeleteMessageHandler>(
                    $"{ExchangeDataUpdateUtils.Models.User}.{ExchangeDataUpdateUtils.Actions.Delete}",
                    ExchangeDataUpdateUtils.ExchangeName);
            ;
            services.AddHostedService<ConsumingHostedService>();
        }
    }
}