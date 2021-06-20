using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security
{
    public class UserContext : IUserContext
    {
        public const string PermissionsClaim = "permissions";
            

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            IsAuthenticated = user?.Identity?.IsAuthenticated ?? false;
            UserId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "ANONYMOUS";
            Name = user?.Identity?.Name ?? "ANONYMOUS";
            Permissions = 
                user?.FindAll(PermissionsClaim).Select(r => r.Value).ToHashSet() ?? new HashSet<string>();
        }

        public bool IsAuthenticated { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public ICollection<string> Permissions { get; set; }
    }
}
