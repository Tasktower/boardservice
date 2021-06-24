using System.Collections.Generic;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Services;

namespace Tasktower.ProjectService.Tests.TestTools.Helpers
{
    public static class UserContextTestHelperExtensions
    {
        public static void SignOutForTesting(this IUserSecurityContext userSecurityContext)
        {
            userSecurityContext.IsAuthenticated = false;
            userSecurityContext.UserId = "ANONYMOUS";
            userSecurityContext.Name = "ANONYMOUS";
            userSecurityContext.Permissions = new HashSet<string>();
        }

        public static void SignInForTesting(this IUserSecurityContext userSecurityContext, string userId, string name,
            ICollection<string> permissions)
        {
            userSecurityContext.IsAuthenticated = true;
            userSecurityContext.UserId = userId;
            userSecurityContext.Name = name;
            userSecurityContext.Permissions = permissions;
        }
    }
}