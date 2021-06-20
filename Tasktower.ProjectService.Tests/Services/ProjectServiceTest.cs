using System;
using System.Collections.Generic;
using Castle.Core.Internal;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Security;
using Tasktower.ProjectService.Services;
using Tasktower.ProjectService.Tests.TestTools.Helpers;
using Tasktower.ProjectService.Tools.Paging;
using Xunit;

namespace Tasktower.ProjectService.Tests.Services
{
    public sealed class ProjectServiceTest : IDisposable
    {
        private const string User1Id = "12345";
        private const string User1Name = "John Stewart";
        private readonly HashSet<string> User1Permissions = new() 
            {Permissions.DeleteProjectsAny, Permissions.ReadProjectsAny, Permissions.UpdateProjectsAny};
        
        private readonly IProjectsService _projectsService;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        
        public ProjectServiceTest(IProjectsService projectsService, IUserContext userContext, IUnitOfWork unitOfWork)
        {
            _projectsService = projectsService;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }
        
        public void Dispose()
        {
            _userContext.SignOutForTesting();
            _unitOfWork.ProjectRepository.DeleteAll().AsTask().Wait();
            _unitOfWork.SaveChanges().AsTask().Wait();
        }

        [Fact]
        public async void CreateProject_AsSignedInUserAndWithRequiredFields_ProjectCanBeQueried()
        {
            _userContext.SignInForTesting(User1Id, User1Name, User1Permissions);
            var projectSave = new ProjectSaveDto
            {
                Title = "Make App",
                Description = "Make app for users"
            };
            
            var createResult = await _projectsService.CreateNewProject(projectSave);
            var queriedResult = await _projectsService.FindProjectById(createResult.Id);
            
            Assert.Equal(createResult.Id, queriedResult.Project.Id);
            Assert.Equal(projectSave.Title, queriedResult.Project.Title);
            Assert.Equal(projectSave.Description, queriedResult.Project.Description);
            Assert.Equal(User1Name, queriedResult.Project.CreatedBy);
            Assert.Equal(User1Name, queriedResult.Project.ModifiedBy);
            Assert.Equal(User1Id, queriedResult.ProjectOwnerId);
        }
        
        [Fact]
        public async void ReadAnyProjects_NoProjectsIncludedAndNotMember_NoProjects()
        {
            var pagination = new Pagination
            {
                Page = 0,
                PageSize = 100
            };
            var projects = await _projectsService.FindProjectsPage(pagination, false);
            Assert.Equal(0, projects.ResultsSize);
            Assert.Equal(0, projects.Total);
            Assert.Equal(0, projects.Pagination.Page);
            Assert.True(projects.ResultsList.IsNullOrEmpty());
        }
    }
}