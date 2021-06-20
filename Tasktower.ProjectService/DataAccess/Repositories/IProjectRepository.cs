using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;
using Tasktower.ProjectService.Tools.Paging;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public interface IProjectRepository : ICrudRepository<Guid, ProjectEntity>
    {
        public ValueTask<ProjectEntity> FindProjectByIdWithProjectRoles(Guid projectId);

        public ValueTask<Page<ProjectEntity>> FindAllProjectsWithRoles(Pagination pagination);

        public ValueTask<Page<ProjectEntity>> FindProjects(Pagination pagination, string search, 
            ICollection<string> ownerIds, string userId, bool pendingInvites, bool member, bool authorized);
    }
}
