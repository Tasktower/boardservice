using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Tasktower.ProjectService.Security
{
    public static class Policies
    {
        public sealed class Policy
        {
            public string Name { get; }
            public IEnumerable<string> Roles { get; }
            public Policy(string name, IEnumerable<IEnumerable<string>> roleGroups)
            {
                Name = name;
                Roles = roleGroups.SelectMany(g => g).Distinct();
            }
        }
        
        public static class Names
        {
            public const string Admin = "Admin";
            public const string CanModerate = "CanModerate";
        }

        public static IEnumerable<Policy> Get()
        {
            return new List<Policy>()
            {
                new Policy(Names.Admin, new[]
                {
                    Roles.AdminGroup()
                }),
                new Policy(Names.CanModerate, new[]
                {
                    Roles.AdminGroup(), Roles.ModeratorGroup()
                })
            };
        }
        
    }
}
