using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasktower.Lib.Aspnetcore.DataAccess.Repositories;
using Tasktower.Lib.Aspnetcore.Tools.Paging;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public interface IProjectRepository : ICrudRepository<Guid, ProjectEntity>
    {
        public ValueTask<ProjectEntity> FindProjectByIdWithProjectRoles(Guid projectId);

        public ValueTask<Page<ProjectEntity>> FindAllProjectsWithRoles(Pagination pagination);

        public ValueTask<Page<ProjectEntity>> FindProjects(Pagination pagination, string search, 
            ICollection<string> ownerIds, string userId, bool pendingInvites, bool member, bool authorizedProjects);
    }
}
