﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Tasktower.Lib.Aspnetcore.Paging;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Services.Impl
{
    public class ProjectsService : IProjectsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        private readonly IErrorService _errorService;
        private readonly IProjectAuthorizeService _projectAuthorizeService;
        private readonly IValidationService _validationService;
        
        public ProjectsService(IUserContextService userContextService, IUnitOfWork unitOfWork, 
            ILogger<ProjectsService> logger, IMapper mapper, IErrorService errorService,
            IProjectAuthorizeService projectAuthorizeService, IValidationService validationService)
        {
            _userContextService = userContextService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _errorService = errorService;
            _projectAuthorizeService = projectAuthorizeService;
            _validationService = validationService;
        }

        public async ValueTask<ProjectReadDto> CreateNewProject(ProjectSaveDto projectSaveDto)
        {
            var projectEntity = _mapper.Map<ProjectSaveDto, ProjectEntity>(projectSaveDto);
            var projectRoleEntity = new ProjectRoleEntity()
            {
                UserId = _userContextService.UserId,
                Role = ProjectRoleValue.OWNER,
                PendingInvite = false
            };
            projectEntity.ProjectRoles = new List<ProjectRoleEntity>() {projectRoleEntity};
            await _unitOfWork.ProjectRepository.Insert(projectEntity);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectEntity, ProjectReadDto>(projectEntity);
        }
        
        public async ValueTask<ProjectReadDto> UpdateProject(Guid id, ProjectSaveDto projectSaveDto, bool authorize = true)
        {
            var oldProjectEntity = await _unitOfWork.ProjectRepository.GetById(id);
            if (oldProjectEntity == null)
            {
                throw _errorService.Create(ErrorCode.ProjectIdNotFound, id);
            }
            if (authorize)
            {
                await _projectAuthorizeService.Authorize(id, _projectAuthorizeService.WriterRoles());
            }

            var projectEntityToSave = _mapper.Map(projectSaveDto, oldProjectEntity);
            await _unitOfWork.ProjectRepository.Update(projectEntityToSave);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectEntity, ProjectReadDto>(projectEntityToSave);
        }
        
        public async ValueTask<ProjectReadDto> DeleteProject(Guid id, bool member = true)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(id);
            if (project == null)
            {
                throw _errorService.Create(ErrorCode.ProjectIdNotFound, id);
            }
            if (member)
            {
                await _projectAuthorizeService.Authorize(id, _projectAuthorizeService.OwnerRoles());
            }
            await _unitOfWork.ProjectRepository.Delete(id);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectEntity, ProjectReadDto>(project);
        }

        public async ValueTask<ProjectSearchDto> FindProjectById(Guid id, bool authorize = true)
        {
            var project = await _unitOfWork.ProjectRepository.FindProjectByIdWithProjectRoles(id);
            if (project == null)
            {
                throw _errorService.Create(ErrorCode.ProjectIdNotFound, id);
            }
            if (authorize)
            {
                await _projectAuthorizeService.Authorize(id, _projectAuthorizeService.ReaderRoles(), true);
            }
            
            return ProjectSearchDtoFromProject(project);
        }

        public async ValueTask<Page<ProjectSearchDto>> FindProjects(Pagination pagination, string search, 
            ICollection<string> ownerIds, bool pendingInvites, bool member, bool authorizedProjects = true)
        {
            var projectsPage = await _unitOfWork.ProjectRepository.FindProjects(pagination, search, ownerIds, 
                _userContextService.UserId, pendingInvites, member, authorizedProjects);
            return projectsPage.Map(ProjectSearchDtoFromProject);
        }

        private ProjectSearchDto ProjectSearchDtoFromProject(ProjectEntity project)
        {
            var projectReadDto = _mapper.Map<ProjectEntity, ProjectReadDto>(project);
            var owner = (from pr in project.ProjectRoles 
                where ProjectRoleValue.OWNER == pr.Role 
                select pr.UserId).FirstOrDefault();
            return new ProjectSearchDto {ProjectOwnerId = owner, Project = projectReadDto};
        }
    }
}