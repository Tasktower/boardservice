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
            public string Name { get; set; }
            public IEnumerable<string> Permissions { get; set; }
        }
        
        public static class Names
        {
            public const string ReadProjectsAny = "ReadProjectsAny";
            public const string UpdateProjectsAny = "UpdateProjectsAny";
            public const string DeleteProjectsAny = "DeleteProjectsAny";
        }

        public static IEnumerable<Policy> Get()
        {
            return new List<Policy>()
            {
                new()
                {
                    Name = Names.ReadProjectsAny,
                    Permissions = new[]
                    {
                        Permissions.ReadProjectsAny
                    }
                },
                new()
                {
                    Name = Names.UpdateProjectsAny,
                    Permissions = new[]
                    {
                        Permissions.UpdateProjectsAny
                    }
                },
                new()
                {
                    Name = Names.DeleteProjectsAny,
                    Permissions = new[]
                    {
                        Permissions.DeleteProjectsAny
                    }
                }
            };
        }
        
    }
}
