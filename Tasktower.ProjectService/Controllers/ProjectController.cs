using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Security;
using Tasktower.ProjectService.Services;
using Tasktower.ProjectService.Tools.Paging;

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
        [HttpPut("{id}")]
        public async ValueTask<ProjectReadDto> UpdateProject([FromRoute] Guid id,
            [FromBody] ProjectSaveDto projectSaveDto)
        {
            return await _projectsService.UpdateProject(id, projectSaveDto);
        }
        
        [Authorize(Policy = Policies.Names.UpdateProjectsAny)]
        [HttpPut("{id}/any")]
        public async ValueTask<ProjectReadDto> UpdateProjectAny([FromRoute] Guid id,
            [FromBody] ProjectSaveDto projectSaveDto)
        {
            return await _projectsService.UpdateProject(id, projectSaveDto, false);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async ValueTask<ProjectReadDto> DeleteProject([FromRoute] Guid id)
        {
            return await _projectsService.DeleteProject(id);
        }
        
        [Authorize(Policy = Policies.Names.DeleteProjectsAny)]
        [HttpDelete("{id}/any")]
        public async ValueTask<ProjectReadDto> DeleteProjectAny([FromRoute] Guid id)
        {
            return await _projectsService.DeleteProject(id, false);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async ValueTask<ProjectSearchDto> GetById([FromRoute] Guid id)
        {
            return await _projectsService.FindProjectById(id);
        }
        
        [Authorize(Policy = Policies.Names.ReadProjectsAny)]
        [HttpGet("{id}/any")]
        public async ValueTask<ProjectSearchDto> GetByIdAny([FromRoute] Guid id)
        {
            return await _projectsService.FindProjectById(id, false);
        }
        
        [Authorize]
        [HttpGet]
        public async ValueTask<Page<ProjectSearchDto>> GetPage([FromQuery] Pagination pagination)
        {
            return await _projectsService.FindProjectsPage(pagination);
        }
        
        [Authorize(Policy = Policies.Names.ReadProjectsAny)]
        [HttpGet("any")]
        public async ValueTask<Page<ProjectSearchDto>> GetPageAny([FromQuery] Pagination pagination)
        {
            return await _projectsService.FindProjectsPage(pagination, false);
        }
        
        [Authorize]
        [HttpGet("member")]
        public async ValueTask<Page<ProjectSearchDto>> GetMemberProjects([FromQuery] Pagination pagination)
        {
            return await _projectsService.FindMemberProjects(pagination);
        }
        
        [Authorize]
        [HttpGet("prendinginvite")]
        public async ValueTask<Page<ProjectSearchDto>> GetPendingInviteProjects([FromQuery] Pagination pagination)
        {
            return await _projectsService.FindPendingInviteProjects(pagination);
        }
    }
}