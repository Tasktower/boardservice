using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security
{
    public static class Policies
    {
        public static class PolicyNames
        {
            public const string Admin = "Admin";
            public const string CanModerate = "CanModerate";
        }

        public static readonly Policy AdministratorPolicy = new Policy(PolicyNames.Admin, policyBuilder => {
            policyBuilder.RequireClaim(System.Security.Claims.ClaimTypes.Role, 
                new []
                    {
                        RoleGroups.AdminGroup()
                    }
                    .SelectMany(g => g).Distinct());
        });

        public static readonly Policy CanModeratePolicy = new Policy(PolicyNames.CanModerate, policyBuilder => {
            policyBuilder.RequireClaim(System.Security.Claims.ClaimTypes.Role, 
                new []
                    {
                        RoleGroups.AdminGroup(), 
                        RoleGroups.ModeratorGroup()
                    }
                    .SelectMany(g => g).Distinct());
        });
    }
}
