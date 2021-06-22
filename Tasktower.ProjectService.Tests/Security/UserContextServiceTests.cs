using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.Lib.Aspnetcore.Services.Impl;
using Tasktower.ProjectService.Tests.TestTools.Helpers;
using Xunit;

namespace Tasktower.ProjectService.Tests.Security
{
    public sealed class UserContextServiceTests : IDisposable
    {
        private readonly IUserContextService _userContextService;

        public UserContextServiceTests(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }
        
        public void Dispose()
        {
            _userContextService.SignOutForTesting();
        }

        [Fact]
        public void UserContext_UserAuthenticated_ReturnAuthenticatedUserInfo()
        {
            const string userId = "123";
            const string name = "John Stewart";
            var permissions = new HashSet<string>() {Permissions.ReadProjectsAny, Permissions.UpdateProjectsAny};
            _userContextService.SignInForTesting(userId, name, permissions);
            Assert.True(_userContextService.IsAuthenticated);
            Assert.Equal(userId, _userContextService.UserId);
            Assert.Equal(name, _userContextService.Name);
            Assert.Collection(_userContextService.Permissions, 
                p => Assert.Equal(p, Permissions.ReadProjectsAny),
                p => Assert.Equal(p, Permissions.UpdateProjectsAny));
        }
        
        [Fact]
        public void UserContext_NoUserAuthenticated_ReturnAnonymousUser()
        {
            Assert.Equal("ANONYMOUS", _userContextService.Name);
            Assert.Equal("ANONYMOUS", _userContextService.UserId);
            Assert.False(_userContextService.IsAuthenticated);
            Assert.Equal(0, _userContextService.Permissions.Count);
        }

        [Fact] public void CreateUserContext_WithAuthenticatedPrinciple_ReturnAnonymousUser()
        {
            const string userId = "123";
            const string name = "John Stewart";
            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new(ClaimTypes.NameIdentifier, userId),
                        new(ClaimTypes.Name, name),
                        new(UserContextService.PermissionsClaim, Permissions.ReadProjectsAny),
                        new(UserContextService.PermissionsClaim, Permissions.UpdateProjectsAny)
                    },
                    "Basic")
            );
            
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(h => h.User).Returns(claimsPrincipal);
        
            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = httpContextMock.Object;
        
            var testUserContext = new UserContextService(httpContextAccessor);
        
            Assert.True(testUserContext.IsAuthenticated);
            Assert.Equal(userId, testUserContext.UserId);
            Assert.Equal(name, testUserContext.Name);
            Assert.Collection(testUserContext.Permissions, 
                p => Assert.Equal(p, Permissions.ReadProjectsAny),
                p => Assert.Equal(p, Permissions.UpdateProjectsAny));
        }

        [Fact]
        public void CreateUserContext_WithEmptyPrinciple_ReturnAnonymousUser()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(h => h.User).Returns(claimsPrincipal);

            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = httpContextMock.Object;

            var testUserContext = new UserContextService(httpContextAccessor);

            Assert.False(claimsPrincipal.Identity?.IsAuthenticated);
            Assert.Equal("ANONYMOUS", testUserContext.Name);
            Assert.Equal("ANONYMOUS", testUserContext.UserId);
            Assert.False(testUserContext.IsAuthenticated);
            Assert.Equal(0, testUserContext.Permissions.Count);
        }
    }
}