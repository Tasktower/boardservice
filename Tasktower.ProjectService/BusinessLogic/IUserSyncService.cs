using System.Threading.Tasks;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Dtos.External;

namespace Tasktower.ProjectService.BusinessLogic
{
    public interface IUserSyncService
    {
        public Task<UserReadDto> UpdateUserFromExternal(ExtUserSyncMessageDto extUserSync);
        public Task DeleteUserFromExternal(ExtUserSyncMessageDto extUserSync);
    }
}