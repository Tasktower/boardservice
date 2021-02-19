using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.Webtools.Security.Auth;

namespace Tasktower.BoardService.Security
{
    public static class Policies
    {
        public const string PolicyNameAdministrator = "Administrator";
        public static readonly Policy AdministratorPolicy = new Policy(PolicyNameAdministrator, policyBuilder => {
            policyBuilder.RequireClaim(System.Security.Claims.ClaimTypes.Role, AccessorUtils.MergeAccessGroups(RoleGroups.AdminGroup()));
        });

        public const string PolicyNameCanModerate = "CanModerate";
        public static readonly Policy CanModeratePolicy = new Policy(PolicyNameCanModerate, policyBuilder => {
            policyBuilder.RequireClaim(System.Security.Claims.ClaimTypes.Role, AccessorUtils.MergeAccessGroups(
                RoleGroups.AdminGroup(), 
                RoleGroups.ModeratorGroup()));
        });
    }
}
