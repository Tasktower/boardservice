﻿using Microsoft.AspNetCore.Http;
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

        public static UserContext FromHttpContext(HttpContext context)
        {
            return new(context);
        }

        private UserContext(HttpContext context)
        {
            _user = context?.User;
        }

        public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

        public string UserId => _user?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "ANONYMOUS";

        public string Name => _user?.Identity?.Name ?? "ANONYMOUS";

        public ICollection<string> Permissions => _user?.FindAll("permissions").Select(r => r.Value).ToHashSet();
    }
}
