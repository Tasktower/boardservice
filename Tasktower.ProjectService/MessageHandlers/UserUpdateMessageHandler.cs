using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using RabbitMQ.Client.Events;
using Tasktower.Lib.Aspnetcore.Tools;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.MessageHandlers
{
    public class UserUpdateMessageHandler : IMessageHandler
    {
        readonly ILogger<UserUpdateMessageHandler> _logger;
        readonly IQueueService _queue;
        public UserUpdateMessageHandler(ILogger<UserUpdateMessageHandler> logger, IQueueService queue)
        {
            _logger = logger;
            _queue = queue;
        }
        
        public void Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            _logger.LogDebug("Route {MatchingRoute}", matchingRoute);
            var jsonMsg = Encoding.UTF8.GetString(eventArgs.Body.Span);
            _logger.LogDebug("json: {0}", jsonMsg);
            var userValue = JsonSerializerUtils.ReadFromJson<ExtUserPublicReadDto>(jsonMsg);
            _logger.LogDebug("userid {0}", userValue.UserId);
            _logger.LogDebug("username {0}", userValue.UserName);
        }
    }
}