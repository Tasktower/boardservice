using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Tools.Paging;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Services;

namespace Tasktower.ProjectService.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        
        public ProjectController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [Authorize]
        [HttpPost]
        public async ValueTask<ProjectReadDto> CreateProject([FromBody] ProjectSaveDto projectSaveDto)
        {
            return await _projectsService.CreateNewProject(projectSaveDto);
        }
        
        [Authorize]
        [HttpPut("{id:guid}")]
        public async ValueTask<ProjectReadDto> UpdateProject([FromRoute] Guid id,
            [FromBody] ProjectSaveDto projectSaveDto)
        {
            return await _projectsService.UpdateProject(id, projectSaveDto);
        }
        
        [Authorize(Policy = Policies.Names.UpdateProjectsAny)]
        [HttpPut("{id:guid}/any")]
        public async ValueTask<ProjectReadDto> UpdateProjectAny([FromRoute] Guid id,
            [FromBody] ProjectSaveDto projectSaveDto)
        {
            return await _projectsService.UpdateProject(id, projectSaveDto, false);
        }
        
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async ValueTask<ProjectReadDto> DeleteProject([FromRoute] Guid id)
        {
            return await _projectsService.DeleteProject(id);
        }
        
        [Authorize(Policy = Policies.Names.DeleteProjectsAny)]
        [HttpDelete("{id:guid}/any")]
        public async ValueTask<ProjectReadDto> DeleteProjectAny([FromRoute] Guid id)
        {
            return await _projectsService.DeleteProject(id, false);
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public async ValueTask<ProjectSearchDto> GetById([FromRoute] Guid id)
        {
            return await _projectsService.FindProjectById(id);
        }
        
        [Authorize(Policy = Policies.Names.ReadProjectsAny)]
        [HttpGet("{id:guid}/any")]
        public async ValueTask<ProjectSearchDto> GetByIdAny([FromRoute] Guid id)
        {
            return await _projectsService.FindProjectById(id, false);
        }
        
        [Authorize]
        [HttpGet]
        public async ValueTask<Page<ProjectSearchDto>> FindProjects([FromQuery] Pagination pagination,
            [FromQuery] string search, [FromQuery] ICollection<string> ownerIds, 
            [FromQuery] bool pendingInvites, [FromQuery] bool member)
        {
            return await _projectsService.FindProjects(pagination, search, ownerIds, pendingInvites, member);
        }
        
        [Authorize(Policy = Policies.Names.ReadProjectsAny)]
        [HttpGet("any")]
        public async ValueTask<Page<ProjectSearchDto>> FindProjectsAny([FromQuery] Pagination pagination,
            [FromQuery] string search, [FromQuery] ICollection<string> ownerIds)
        {
            return await _projectsService.FindProjects(pagination, search, ownerIds, 
                true, true, false);
        }
    }
}