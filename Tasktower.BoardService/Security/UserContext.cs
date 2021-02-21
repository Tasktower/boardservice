using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tasktower.Webtools.DependencyInjection;
using Tasktower.Webtools.Security.Auth;

namespace Tasktower.BoardService.Security
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

        public string Name => _user?.FindFirst("name")?.Value;

        public ICollection<Role> Roles => _user?.FindAll(ClaimTypes.Role)
            .Select(r => Enum.TryParse(r.Value, out Role role) ? role : Role.UKNOWN)
            .ToList();
    }
}
