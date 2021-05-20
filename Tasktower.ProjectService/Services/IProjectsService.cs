using System;
using System.Threading.Tasks;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Tools.Paging;

namespace Tasktower.ProjectService.Services
{
    public interface IProjectsService
    {
        public ValueTask<ProjectReadDto> CreateNewProject(ProjectSaveDto projectSaveDto);
        public ValueTask<ProjectReadDto> UpdateProject(Guid id, ProjectSaveDto projectSaveDto);
        public ValueTask<ProjectReadDto> DeleteProject(Guid id);
        public ValueTask<ProjectReadDto> FindProjectById(Guid id);
        public ValueTask<Page<ProjectReadDto>> FindMemberProjects(Pagination pagination);
        public ValueTask<Page<ProjectReadDto>> FindPendingInviteProjects(Pagination pagination);
        public ValueTask<Page<ProjectReadDto>> FindProjectsPageForUser(Pagination pagination);

    }
}