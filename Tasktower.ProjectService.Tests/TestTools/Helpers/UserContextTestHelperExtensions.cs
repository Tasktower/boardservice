using System.Collections.Generic;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Security.Services;

namespace Tasktower.ProjectService.Tests.TestTools.Helpers
{
    public static class UserContextTestHelperExtensions
    {
        public static void SignOutForTesting(this IUserContextService userContextService)
        {
            userContextService.IsAuthenticated = false;
            userContextService.UserId = "ANONYMOUS";
            userContextService.Name = "ANONYMOUS";
            userContextService.Permissions = new HashSet<string>();
        }

        public static void SignInForTesting(this IUserContextService userContextService, string userId, string name,
            ICollection<string> permissions)
        {
            userContextService.IsAuthenticated = true;
            userContextService.UserId = userId;
            userContextService.Name = name;
            userContextService.Permissions = permissions;
        }
    }
}