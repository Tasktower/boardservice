using System;
using System.Threading.Tasks;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Tools.Paging;

namespace Tasktower.ProjectService.Services
{
    public interface IProjectsService
    {
        public ValueTask<ProjectReadDto> CreateNewProject(ProjectSaveDto projectSaveDto);
        public ValueTask<ProjectReadDto> UpdateProject(Guid id, ProjectSaveDto projectSaveDto, bool authorize = true);
        public ValueTask<ProjectReadDto> DeleteProject(Guid id, bool authorize = true);
        public ValueTask<ProjectSearchDto> FindProjectById(Guid id, bool authorize = true);
        public ValueTask<Page<ProjectSearchDto>> FindMemberProjects(Pagination pagination);
        public ValueTask<Page<ProjectSearchDto>> FindPendingInviteProjects(Pagination pagination);
        public ValueTask<Page<ProjectSearchDto>> FindProjectsPage(Pagination pagination, bool authorized = true);

    }
}