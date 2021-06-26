using System.Threading.Tasks;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Services.External
{
    public interface IExternalUserService
    {
        public Task<ExtUserPublicReadDto> GetUser(string UserId);
    }
}