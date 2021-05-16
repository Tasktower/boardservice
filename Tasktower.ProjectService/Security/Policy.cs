using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security
{
    public sealed class Policy
    {
        public string Name { get; }
        public Action<AuthorizationPolicyBuilder> PolicyBuildAction { get; }
        public Policy(string name, Action<AuthorizationPolicyBuilder> policyBuildAction)
        {
            Name = name;
            PolicyBuildAction = policyBuildAction;
        }
    }
}
