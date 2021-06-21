using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasktower.Lib.Aspnetcore.DataAccess.Helpers;
using Tasktower.Lib.Aspnetcore.DataAccess.Repositories;
using Tasktower.Lib.Aspnetcore.Paging;
using Tasktower.Lib.Aspnetcore.Paging.Extensions;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class ProjectRepository : 
        CrudRepositoryEfCore<Guid, ProjectEntity, BoardDBContext>, 
        IProjectRepository
    {
        public ProjectRepository(BoardDBContext context) : base(context) { }

        public async ValueTask<ProjectEntity> FindProjectByIdWithProjectRoles(Guid projectId)
        {
            return await (from project in dbSet.AsQueryable()
                    where project.Id == projectId
                    select project)
                .Include(p => p.ProjectRoles)
                .SingleOrDefaultAsync();
        }

        public async ValueTask<Page<ProjectEntity>> FindAllProjectsWithRoles(Pagination pagination)
        {
            return await dbSet.AsQueryable().Include(p => p.ProjectRoles)
                .GetPageAsync(pagination, ProjectEntity.OrderByQuery);
        }

        public async ValueTask<Page<ProjectEntity>> FindProjects(Pagination pagination, string search, 
            ICollection<string> ownerIds, string userId, bool pendingInvites, bool member, bool authorizedProjects)
        {
            ownerIds ??= new List<string>();

            return await (from project in dbSet.AsQueryable()
                    join projectRole in context.ProjectRoles.AsQueryable()
                        on project.Id equals projectRole.ProjectId
                    where (!authorizedProjects || 
                           projectRole != null &&
                           projectRole.UserId == userId && 
                           (pendingInvites && projectRole.PendingInvite || 
                            member && !projectRole.PendingInvite)) 
                          &&
                          (search == null || 
                           EF.Functions.Like( project.Title, QueryUtils.LikeWrap(search)) || 
                           EF.Functions.Like( project.Description, QueryUtils.LikeWrap(search)))
                    select project)
                .Include(p => p.ProjectRoles)
                .Where(p => ownerIds.Count == 0 || 
                            p.ProjectRoles.Any(pr => ownerIds.Contains(pr.UserId)))
                .GetPageAsync(pagination, ProjectEntity.OrderByQuery);
        }
    }
}
