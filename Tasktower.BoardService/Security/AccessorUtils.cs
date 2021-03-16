using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Security
{
    // Accessor is a generat term for any string value that gives you permissions to access a certain resource.
    // An accessor can be a user role or even an oath2 scope.
    public static class AccessorUtils
    {
        /// <summary>
        /// Transform parameters of roles into an IEnumerable of accessor strings.
        /// This functions as an accessor group.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static IEnumerable<string> RolesToAccessorGroup(params string[] roles)
        {
            return roles.Distinct().Select(r => $"{r}");
        }

        /// <summary>
        /// Groups parameters of accessor strings into an accessor group.
        /// </summary>
        /// <param name="accessors"></param>
        /// <returns></returns>
        public static IEnumerable<string> GroupAccessors(params string[] accessors) {
            return accessors.Distinct();
        }

        public static IEnumerable<string> MergeAccessGroups(params IEnumerable<string>[] groups)
        {
            return groups.SelectMany(g => g).Distinct();
        }

        public static string AccessEnumerableToAuthString(IEnumerable<string> accessors)
        {
            return string.Join(",", accessors);
        }
    }
}
