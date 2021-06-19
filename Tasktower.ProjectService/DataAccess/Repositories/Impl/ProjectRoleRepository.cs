using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class ProjectRoleRepository : 
        CrudRepositoryImpl<Guid, ProjectRoleEntity, BoardDBContext>, IProjectRoleRepository
    {
        public ProjectRoleRepository(BoardDBContext context) : base(context) { }

        public async ValueTask<bool> UserHasProjectRolePermission(Guid projectId, string userId, 
            ISet<ProjectRoleValue> projectRoles, bool allowPendingInvite = false)
        {
            return await (from p in dbSet.AsQueryable()
                where p.ProjectId == projectId &&
                      p.UserId == userId &&
                      (allowPendingInvite || !p.PendingInvite) &&
                      projectRoles.Contains(p.Role)
                select p).AnyAsync();
        }

        public async ValueTask<ProjectRoleEntity> findProjectOwner(Guid projectId)
        {
            return await (from p in dbSet.AsQueryable()
                where p.ProjectId == projectId &&
                      p.Role == ProjectRoleValue.OWNER
                select p).FirstAsync();
        }
    }
}
