using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.Webtools.Security.Auth;

namespace Tasktower.BoardService.Security
{
    public class RoleGroups
    {
        public static IEnumerable<string> AdminGroup()
        {
            return AccessorUtils.RolesToAccessorGroup(Role.ADMIN);
        }

        public static IEnumerable<string> ModeratorGroup()
        {
            return AccessorUtils.RolesToAccessorGroup(Role.MODERATOR);
        }
    }
}
