using Microsoft.AspNetCore.Http;
using Tasktower.ProjectService.Security;

namespace Tasktower.ProjectService.Services.Impl
{
    public class UserContextService : IUserContextService
    {
        private readonly UserContext _userContext; 
        
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _userContext = UserContext.FromHttpContext(httpContextAccessor.HttpContext);
        }

        public UserContext Get()
        {
            return _userContext;
        }
    }
}