using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Tasktower.Lib.Aspnetcore.Errors;
using Tasktower.Lib.Aspnetcore.Paging;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Services;
using Tasktower.ProjectService.Tests.TestTools.Helpers;
using Tasktower.ProjectService.Tools.Constants;
using Xunit;

namespace Tasktower.ProjectService.Tests.Services
{
    public sealed class ProjectServiceTest : IDisposable
    {
        // User 1 Data
        private const string User1Id = "10001";
        private const string User1Name = "John Stewart";
        private readonly HashSet<string> _user1Permissions = new() 
            {Permissions.DeleteProjectsAny, Permissions.ReadProjectsAny, Permissions.UpdateProjectsAny};
        
        private readonly Guid _project1AId = Guid.Parse("0eca324e-a333-4e68-9b0a-ebeef00b60ec");
        private const string Project1ATitle = "Project 1A";
        private const string Project1ADescription = "Project 1A description";
        
        private readonly Guid _project1BId = Guid.Parse("9cd8a7ba-a6c8-44da-b310-d9a8afca525f");
        private const string Project1BTitle = "Project 1B";
        private const string Project1BDescription = "Project 1B description";
        
        // User 2 Data
        private const string User2Id = "10002";
        private const string User2Name = "Steve Rodgers";
        private readonly HashSet<string> _user2Permissions = new() {Permissions.ReadProjectsAny};
        
        private readonly Guid _project2AId = Guid.Parse("2fceb781-c31c-4f47-9689-3916ad0bcbe8");
        private const string Project2ATitle = "Project 2A";
        private const string Project2ADescription = "Project 2A description";
        
        // User 3 Data
        private const string User3Id = "10003";
        private const string User3Name = "Gorilla Grodd";
        private readonly HashSet<string> _user3Permissions = new();
        
        private readonly Guid _project3AId = Guid.Parse("e52a6e70-1ffd-4439-8f8a-ba8dbfea9efe");
        private const string Project3ATitle = "Project 3A";
        private const string Project3ADescription = "Project 3A description";
        
        private readonly Guid _project3BId = Guid.Parse("9c831b5f-6956-4954-9bc3-7c325bbfe0c2");
        private const string Project3BTitle = "Project 3B";
        private const string Project3BDescription = "Project 3B description";

        private const int ProjectsTotal = 5;

        private readonly IProjectsService _projectsService;
        private readonly IUserContextService _userContextService;
        private readonly IUnitOfWork _unitOfWork;
        
        public ProjectServiceTest(IProjectsService projectsService, IUserContextService userContextService, IUnitOfWork unitOfWork)
        {
            _projectsService = projectsService;
            _userContextService = userContextService;
            _unitOfWork = unitOfWork;
            InitData().Wait();
        }
        
        public void Dispose()
        {
            _userContextService.SignOutForTesting();
            _unitOfWork.ProjectRepository.DeleteAll().AsTask().Wait();
            _unitOfWork.SaveChanges().AsTask().Wait();
        }

        private async Task InitData()
        {
            // User 1 projects
            _userContextService.SignInForTesting(User1Id, User1Name, _user1Permissions);
            var user1Projects = new[]
            {
                new ProjectEntity
                {
                    Id = _project1AId,
                    Title = Project1ATitle,
                    Description = Project1ADescription,
                    ProjectRoles = new [] {NewProjectOwnerRoleFromContext()}
                },
                new ProjectEntity
                {
                    Id = _project1BId,
                    Title = Project1BTitle,
                    Description = Project1BDescription,
                    ProjectRoles = new [] {NewProjectOwnerRoleFromContext()}
                }
            };
            await _unitOfWork.ProjectRepository.InsertMany(user1Projects);
            // User 2 projects
            _userContextService.SignInForTesting(User2Id, User2Name, _user2Permissions);
            await _unitOfWork.ProjectRepository.InsertMany(new[]
            {
                new ProjectEntity
                {
                    Id = _project2AId,
                    Title = Project2ATitle,
                    Description = Project2ADescription,
                    ProjectRoles = new [] {NewProjectOwnerRoleFromContext()}
                }
            });
            // User 3 projects
            _userContextService.SignInForTesting(User3Id, User3Name, _user3Permissions);
            await _unitOfWork.ProjectRepository.InsertMany(new[]
            {
                new ProjectEntity
                {
                    Id = _project3AId,
                    Title = Project3ATitle,
                    Description = Project3ADescription,
                    ProjectRoles = new [] {NewProjectOwnerRoleFromContext()}
                },
                new ProjectEntity
                {
                    Id = _project3BId,
                    Title = Project3BTitle,
                    Description = Project3BDescription,
                    ProjectRoles = new [] {NewProjectOwnerRoleFromContext()}
                }
            });
            // Sign out user
            _unitOfWork.SaveChanges().AsTask().Wait();
            _userContextService.SignOutForTesting();
        }
        
        private ProjectRoleEntity NewProjectOwnerRoleFromContext()
        {
            return new()
            {
                UserId = _userContextService.UserId,
                Role = ProjectRoleValue.OWNER,
                PendingInvite = false
            };
        }

        [Fact]
        public async void CreateProject_AsSignedInUserAndWithRequiredFields_ProjectCanBeQueried()
        {
            _userContextService.SignInForTesting(User1Id, User1Name, _user1Permissions);
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
        public async void FindAnyProjects_SearchForThreeProjectsPageOne_ThreeProjectsFound()
        {
            var pagination = new Pagination
            {
                PageNumber = 0,
                PageSize = 3
            };
            var projects = await _projectsService.FindProjects(pagination, null, null,
                true, true, false);
            Assert.Equal(3, projects.ContentSize);
            Assert.Equal(ProjectsTotal, projects.Total);
            Assert.Equal(0, projects.Pagination.PageNumber);
            Assert.False(projects.Content.IsNullOrEmpty());
        }
        
        [Fact]
        public async void FindProjectById_Project1AIdAsUser1_ReturnProject()
        {
            _userContextService.SignInForTesting(User1Id, User1Name, _user1Permissions);
            var projectSearchDto = await _projectsService.FindProjectById(_project1AId);
            Assert.Equal(User1Id, projectSearchDto.ProjectOwnerId);
            Assert.Equal(_project1AId, projectSearchDto.Project.Id);
            Assert.Equal(Project1ATitle, projectSearchDto.Project.Title);
            Assert.Equal(Project1ADescription, projectSearchDto.Project.Description);
       }
        
        [Fact]
        public async void FindProjectById_Project1AIdAsUser2_ThrowForbidden()
        {
            _userContextService.SignInForTesting(User2Id, User2Name, _user2Permissions);
            var exception = await Assert.ThrowsAsync<AppException<ErrorCode>>(async () =>
            {
                await _projectsService.FindProjectById(_project1AId);
            });
            Assert.Equal(ErrorCode.NoProjectPermissions, exception.ErrorCode);
            Assert.Equal(HttpStatusCode.Forbidden, exception.StatusCode);
        }
        
        [Fact]
        public async void FindProjectById_UseIdNotInDatabase_ThrowNotFound()
        {
            var projectId = Guid.Parse("44a8079d-ef92-434c-a3d2-aaa4848c4396");
            var exception = await Assert.ThrowsAsync<AppException<ErrorCode>>(async () =>
            {
                await _projectsService.FindProjectById(projectId, false);
            });
            Assert.Equal(ErrorCode.ProjectIdNotFound, exception.ErrorCode);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
            Assert.Equal($"Project with id {projectId.ToString()} not found", exception.Message);
        }
    }
}