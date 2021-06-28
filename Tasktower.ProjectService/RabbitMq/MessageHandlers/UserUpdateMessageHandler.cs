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
            var userMessage = JsonSerializerUtils.ReadFromJson<ExtUserPublicReadDto>(jsonMsg);
            // Todo move to a user service
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
            var mapper = scope.ServiceProvider.GetService<IMapper>();
            var existingUserEntity = await unitOfWork.UserRepository.GetById(userMessage.UserId);
            if (existingUserEntity == null)
            {
                var userEntityToCreate = mapper.Map<UserEntity>(userMessage);
                await unitOfWork.UserRepository.Insert(userEntityToCreate);
            }
            else
            {
                var userEntityToUpdate = mapper.Map(userMessage, existingUserEntity);
                await unitOfWork.UserRepository.Update(userEntityToUpdate);
            }

            await unitOfWork.SaveChanges();
            _logger.LogDebug("updated user {0}", userMessage.UserId);
        }
    }
}