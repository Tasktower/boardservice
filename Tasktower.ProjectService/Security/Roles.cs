using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security
{
    public static class Roles
    {
        public const string ADMIN = "ADMIN";
        public const string MODERATOR = "MODERATOR";
        
        public static IEnumerable<string> AdminGroup()
        {
            return new[] {ADMIN};
        }

        public static IEnumerable<string> ModeratorGroup()
        {
            return new[] {MODERATOR};
        }
    }
}
