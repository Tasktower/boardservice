using Tasktower.ProjectService.Security;

namespace Tasktower.ProjectService.Services
{
    public interface IUserContextService
    {
        public UserContext Get();
    }
}