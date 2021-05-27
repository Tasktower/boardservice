using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public interface IProjectRoleRepository : ICrudRepository<Guid, ProjectRoleEntity>
    {
        /// <summary>
        /// Returns a value task indicating if the user has project permissions
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="projectRoles">Permissions the user is tested against to see if any match</param>
        /// <returns></returns>
        ValueTask<bool> UserHasProjectRolePermission(Guid id, 
            string userId, ISet<ProjectRoleValue> projectRoles);
    }
}
