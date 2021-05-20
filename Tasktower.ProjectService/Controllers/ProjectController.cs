using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasktower.ProjectService.Dtos;
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
        
        [Authorize]
        [HttpDelete("{id}")]
        public async ValueTask<ProjectReadDto> UpdateProject([FromRoute] Guid id)
        {
            return await _projectsService.DeleteProject(id);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async ValueTask<ProjectReadDto> GetById([FromRoute] Guid id)
        {
            return await _projectsService.FindProjectById(id);
        }
        
        [Authorize]
        [HttpGet]
        public async ValueTask<Page<ProjectReadDto>> GetPage([FromQuery] Pagination pagination)
        {
            return await _projectsService.FindProjectsPageForUser(pagination);
        }
        
        [Authorize]
        [HttpGet("member")]
        public async ValueTask<Page<ProjectReadDto>> GetMemberProjects([FromQuery] Pagination pagination)
        {
            return await _projectsService.FindMemberProjects(pagination);
        }
        
        [Authorize]
        [HttpGet("prendinginvite")]
        public async ValueTask<Page<ProjectReadDto>> GetPendingInviteProjects([FromQuery] Pagination pagination)
        {
            return await _projectsService.FindPendingInviteProjects(pagination);
        }
    }
}