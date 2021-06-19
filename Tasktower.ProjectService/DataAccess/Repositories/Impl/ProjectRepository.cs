using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Tools.Constants;
using Tasktower.ProjectService.Tools.Paging;
using Tasktower.ProjectService.Tools.Paging.Extensions;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class ProjectRepository : 
        CrudRepositoryImpl<Guid, ProjectEntity, BoardDBContext>, 
        IProjectRepository
    {
        public ProjectRepository(BoardDBContext context) : base(context) { }

        public async ValueTask<ProjectEntity> FindProjectByIdWithProjectRoles(Guid projectId)
        {
            return await (from project in dbSet.AsQueryable()
                    where project.Id == projectId
                    select project)
                .Include(p => p.ProjectRoles)
                .FirstAsync();
        }

        public async ValueTask<Page<ProjectEntity>> FindAllProjectsWithRoles(Pagination pagination)
        {
            return await dbSet.AsQueryable().Include(p => p.ProjectRoles)
                .GetPageAsync(pagination, ProjectEntity.OrderByQuery);
        }

        public async ValueTask<Page<ProjectEntity>> FindMemberAndInvitedProjects(string userId, Pagination pagination)
        {
            return await (from project in dbSet.AsQueryable()
                    join projectRole in context.ProjectRoles.AsQueryable()
                        on project.Id equals projectRole.ProjectId
                    where userId == projectRole.UserId
                    select project)
                .Include(p => p.ProjectRoles)
                .GetPageAsync(pagination, ProjectEntity.OrderByQuery);
        }

        public async ValueTask<Page<ProjectEntity>> FindMemberProjects(string userId, Pagination pagination)
        {
            return await (from project in dbSet.AsQueryable()
                    join projectRole in context.ProjectRoles.AsQueryable()
                        on project.Id equals projectRole.ProjectId
                    where userId == projectRole.UserId && !projectRole.PendingInvite
                    select project)
                .Include(p => p.ProjectRoles)
                .GetPageAsync(pagination, ProjectEntity.OrderByQuery);
        }
        
        public async ValueTask<Page<ProjectEntity>> FindPendingInviteProjects(string userId, Pagination pagination)
        {
            return await (from project in dbSet.AsQueryable()
                    join projectRole in context.ProjectRoles.AsQueryable()
                        on project.Id equals projectRole.ProjectId
                    where userId == projectRole.UserId && projectRole.PendingInvite
                    select project)
                .Include(p => p.ProjectRoles)
                .GetPageAsync(pagination, ProjectEntity.OrderByQuery);
        }
    }
}
