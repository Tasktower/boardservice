using System;
using System.Text;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Events;
using Tasktower.Lib.Aspnetcore.Tools;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Dtos.External;

namespace Tasktower.ProjectService.RabbitMq.MessageHandlers
{
    public class UserDeleteMessageHandler : IAsyncMessageHandler
    {
        private readonly ILogger<UserDeleteMessageHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UserDeleteMessageHandler(ILogger<UserDeleteMessageHandler> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            var jsonMsg = Encoding.UTF8.GetString(eventArgs.Body.Span);
            var userMessage = JsonSerializerUtils.ReadFromJson<ExtUserPublicReadDto>(jsonMsg);
            // Todo move to a user service
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
            var existingUserEntity = await unitOfWork.UserRepository.GetById(userMessage.UserId);
            if (existingUserEntity == null)
            {
                _logger.LogDebug("user {0} not found", userMessage.UserId);
            }
            else
            {
                await unitOfWork.UserRepository.Delete(existingUserEntity);
            }
            await unitOfWork.SaveChanges();
            _logger.LogDebug("deleted user {0}", userMessage.UserId);
        }
    }
}