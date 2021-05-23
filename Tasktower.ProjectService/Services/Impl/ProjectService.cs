﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Tools.Constants;
using Tasktower.ProjectService.Tools.Paging;

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
        
        public ProjectsService(IUserContextService userContextService, IUnitOfWork unitOfWork, 
            ILogger<ProjectsService> logger, IMapper mapper, IErrorService errorService,
            IProjectAuthorizeService projectAuthorizeService)
        {
            _userContextService = userContextService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _errorService = errorService;
            _projectAuthorizeService = projectAuthorizeService;
        }

        public async ValueTask<ProjectReadDto> CreateNewProject(ProjectSaveDto projectSaveDto)
        {
            var projectEntity = _mapper.Map<ProjectSaveDto, ProjectEntity>(projectSaveDto);
            var projectRoleEntity = new ProjectRoleEntity()
            {
                UserId = _userContextService.Get().UserId,
                Role = ProjectRoleValue.OWNER,
                PendingInvite = false
            };
            projectEntity.ProjectRoles = new List<ProjectRoleEntity>() {projectRoleEntity};
            await _unitOfWork.ProjectRepository.Insert(projectEntity);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectEntity, ProjectReadDto>(projectEntity);
        }
        
        public async ValueTask<ProjectReadDto> UpdateProject(Guid id, ProjectSaveDto projectSaveDto)
        {
            var oldProjectEntity = await _unitOfWork.ProjectRepository.GetById(id);
            if (oldProjectEntity == null)
            {
                throw _errorService.Create(ErrorCode.PROJECT_ID_NOT_FOUND, id);
            }
            await _projectAuthorizeService.Authorize(id, _projectAuthorizeService.WriterRoles());

            var projectEntityToSave = _mapper.Map(projectSaveDto, oldProjectEntity);
            await _unitOfWork.ProjectRepository.Update(projectEntityToSave);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectEntity, ProjectReadDto>(projectEntityToSave);
        }
        
        public async ValueTask<ProjectReadDto> DeleteProject(Guid id)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(id);
            if (project == null)
            {
                throw _errorService.Create(ErrorCode.PROJECT_ID_NOT_FOUND, id);
            }
            await _projectAuthorizeService.Authorize(id, _projectAuthorizeService.OwnerRoles());
            await _unitOfWork.ProjectRepository.Delete(id);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectEntity, ProjectReadDto>(project);
        }

        public async ValueTask<ProjectReadDto> FindProjectById(Guid id)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(id);
            if (project == null)
            {
                throw _errorService.Create(ErrorCode.PROJECT_ID_NOT_FOUND, id);
            }
            return _mapper.Map<ProjectEntity, ProjectReadDto>(project);
        }
        
        public async ValueTask<Page<ProjectReadDto>> FindMemberProjects(Pagination pagination)
        {
            var userContext = _userContextService.Get();
            var projectsPage = await _unitOfWork.ProjectRepository
                .FindMemberProjects(userContext.UserId, pagination);
            return projectsPage.Map(p => _mapper.Map<ProjectReadDto>(p));
        }
        
        public async ValueTask<Page<ProjectReadDto>> FindPendingInviteProjects(Pagination pagination)
        {
            var userContext = _userContextService.Get();
            var projectsPage = await _unitOfWork.ProjectRepository
                .FindPendingInviteProjects(userContext.UserId, pagination);
            return projectsPage.Map(p => _mapper.Map<ProjectReadDto>(p));
        }
        
        public async ValueTask<Page<ProjectReadDto>> FindProjectsPageForUser(Pagination pagination)
        {
            var userContext = _userContextService.Get();
            var projectsPage = await _unitOfWork.ProjectRepository
                .FindMemberProjectsWithUser(userContext.UserId, pagination);
            return projectsPage.Map(p => _mapper.Map<ProjectReadDto>(p));
        }
    }
}