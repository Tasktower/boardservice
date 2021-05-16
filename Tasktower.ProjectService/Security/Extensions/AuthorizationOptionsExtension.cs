using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security.Extensions
{
    public static class AuthorizationOptionsExtension
    {
        public static void AddPolicy(this AuthorizationOptions options, Policy policy)
        {
            options.AddPolicy(policy.Name, policy.PolicyBuildAction);
        }
    }
}
