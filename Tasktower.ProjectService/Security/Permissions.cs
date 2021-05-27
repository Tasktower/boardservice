using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security
{
    public static class Permissions
    {
        public const string ReadProjectsAny = "read:projects_any";
        public const string DeleteProjectsAny = "delete:projects_any";
        public const string UpdateProjectsAny = "update:projects_any";
    }
}
