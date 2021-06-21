using System.Collections.Generic;
using Tasktower.Lib.Aspnetcore.Security;

namespace Tasktower.ProjectService.Tests.TestTools.Helpers
{
    public static class UserContextTestHelperExtensions
    {
        public static void SignOutForTesting(this IUserContext userContext)
        {
            userContext.IsAuthenticated = false;
            userContext.UserId = "ANONYMOUS";
            userContext.Name = "ANONYMOUS";
            userContext.Permissions = new HashSet<string>();
        }

        public static void SignInForTesting(this IUserContext userContext, string userId, string name,
            ICollection<string> permissions)
        {
            userContext.IsAuthenticated = true;
            userContext.UserId = userId;
            userContext.Name = name;
            userContext.Permissions = permissions;
        }
    }
}