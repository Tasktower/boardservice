using System;
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
        public ValueTask<Page<ProjectEntity>> FindMemberAndInvitedProjects(string userId, Pagination pagination);

        public ValueTask<Page<ProjectEntity>> FindMemberProjects(string userId, Pagination pagination);

        public ValueTask<Page<ProjectEntity>> FindPendingInviteProjects(string userId, Pagination pagination);
    }
}
