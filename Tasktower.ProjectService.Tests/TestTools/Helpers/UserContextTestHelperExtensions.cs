using System.Collections.Generic;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.Lib.Aspnetcore.Services.Contexts;

namespace Tasktower.ProjectService.Tests.TestTools.Helpers
{
    public static class UserContextTestHelperExtensions
    {
        public static void SignOutForTesting(this IUserContextAccessorService userContextAccessorService)
        {
            userContextAccessorService.UserContext.IsAuthenticated = false;
            userContextAccessorService.UserContext.UserId = "ANONYMOUS";
            userContextAccessorService.UserContext.Name = "ANONYMOUS";
            userContextAccessorService.UserContext.Permissions = new HashSet<string>();
        }

        public static void SignInForTesting(this IUserContextAccessorService userContextAccessorService, string userId, string name,
            ICollection<string> permissions)
        {
            userContextAccessorService.UserContext.IsAuthenticated = true;
            userContextAccessorService.UserContext.UserId = userId;
            userContextAccessorService.UserContext.Name = name;
            userContextAccessorService.UserContext.Permissions = permissions;
        }
    }
}