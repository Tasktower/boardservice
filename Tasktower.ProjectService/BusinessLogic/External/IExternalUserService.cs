using System.Threading.Tasks;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Dtos.External;

namespace Tasktower.ProjectService.BusinessLogic.External
{
    public interface IExternalUserService
    {
        public Task<ExtUserProfileReadDto> GetUser(string UserId);
    }
}