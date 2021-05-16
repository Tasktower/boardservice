using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security
{
    public class UserContext
    {
        private readonly ClaimsPrincipal _user;
        public UserContext(HttpContext context)
        {
            _user = context?.User;
        }

        public UserContext(ClaimsPrincipal user)
        {
            _user = user;
        }

        public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

        public string UserId => _user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string Name => _user?.Identity?.Name;

        public ICollection<string> Roles => _user?.FindAll(ClaimTypes.Role)
            .Select(r => r.Value).ToHashSet();
    }
}
