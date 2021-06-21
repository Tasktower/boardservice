using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasktower.Lib.Aspnetcore.Tools.Paging;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Services
{
    public interface IProjectsService
    {
        public ValueTask<ProjectReadDto> CreateNewProject(ProjectSaveDto projectSaveDto);
        public ValueTask<ProjectReadDto> UpdateProject(Guid id, ProjectSaveDto projectSaveDto, bool authorize = true);
        public ValueTask<ProjectReadDto> DeleteProject(Guid id, bool authorize = true);
        public ValueTask<ProjectSearchDto> FindProjectById(Guid id, bool authorize = true);
        public ValueTask<Page<ProjectSearchDto>> FindProjects(Pagination pagination, string search,
            ICollection<string> ownerIds, bool pendingInvites, bool member, bool authorizedProjects = true);
   

    }
}