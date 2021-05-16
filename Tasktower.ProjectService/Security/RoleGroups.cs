using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Security
{
    public class RoleGroups
    {
        public static IEnumerable<string> AdminGroup()
        {
            return new[] {Roles.ADMIN};
        }

        public static IEnumerable<string> ModeratorGroup()
        {
            return new[] {Roles.MODERATOR};
        }
    }
}
