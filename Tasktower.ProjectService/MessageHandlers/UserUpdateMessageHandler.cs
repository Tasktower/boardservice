using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Events;
using Tasktower.Lib.Aspnetcore.Tools;
using Tasktower.ProjectService.BusinessLogic;
using Tasktower.ProjectService.Dtos.External;

namespace Tasktower.ProjectService.MessageHandlers
{
    public class UserUpdateMessageHandler : IAsyncMessageHandler
    {
        private readonly ILogger<UserUpdateMessageHandler> _logger;
        private readonly IServiceProvider _serviceProvider;
        public UserUpdateMessageHandler(ILogger<UserUpdateMessageHandler> logger, 
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        
        public async Task Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            var jsonMsg = Encoding.UTF8.GetString(eventArgs.Body.Span);
            var userMessage = JsonSerializerUtils.ReadFromJson<ExtUserSyncMessageDto>(jsonMsg);
            // Todo move to a user service
            using var scope = _serviceProvider.CreateScope();
            var userSyncService = scope.ServiceProvider.GetService<IUserSyncService>();
            await userSyncService.UpdateUserFromExternal(userMessage);
        }
    }
}