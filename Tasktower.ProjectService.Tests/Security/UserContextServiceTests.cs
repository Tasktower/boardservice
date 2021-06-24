using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.ProjectService.Tests.TestTools.Helpers;
using Xunit;

namespace Tasktower.ProjectService.Tests.Security
{
    public sealed class UserContextServiceTests : IDisposable
    {
        private readonly IUserSecurityContext _userSecurityContext;

        public UserContextServiceTests(IUserSecurityContext userSecurityContext)
        {
            _userSecurityContext = userSecurityContext;
        }
        
        public void Dispose()
        {
            _userSecurityContext.SignOutForTesting();
        }

        [Fact]
        public void UserContext_UserAuthenticated_ReturnAuthenticatedUserInfo()
        {
            const string userId = "123";
            const string name = "John Stewart";
            var permissions = new HashSet<string>() {Permissions.ReadProjectsAny, Permissions.UpdateProjectsAny};
            _userSecurityContext.SignInForTesting(userId, name, permissions);
            Assert.True(_userSecurityContext.IsAuthenticated);
            Assert.Equal(userId, _userSecurityContext.UserId);
            Assert.Equal(name, _userSecurityContext.Name);
            Assert.Collection(_userSecurityContext.Permissions, 
                p => Assert.Equal(p, Permissions.ReadProjectsAny),
                p => Assert.Equal(p, Permissions.UpdateProjectsAny));
        }
        
        [Fact]
        public void UserContext_NoUserAuthenticated_ReturnAnonymousUser()
        {
            Assert.Equal("ANONYMOUS", _userSecurityContext.Name);
            Assert.Equal("ANONYMOUS", _userSecurityContext.UserId);
            Assert.False(_userSecurityContext.IsAuthenticated);
            Assert.Equal(0, _userSecurityContext.Permissions.Count);
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
                        new(UserSecurityContext.PermissionsClaim, Permissions.ReadProjectsAny),
                        new(UserSecurityContext.PermissionsClaim, Permissions.UpdateProjectsAny)
                    },
                    "Basic")
            );
            
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(h => h.User).Returns(claimsPrincipal);
        
            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = httpContextMock.Object;
        
            var testUserContext = new UserSecurityContext(httpContextAccessor);
        
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

            var testUserContext = new UserSecurityContext(httpContextAccessor);

            Assert.False(claimsPrincipal.Identity?.IsAuthenticated);
            Assert.Equal("ANONYMOUS", testUserContext.Name);
            Assert.Equal("ANONYMOUS", testUserContext.UserId);
            Assert.False(testUserContext.IsAuthenticated);
            Assert.Equal(0, testUserContext.Permissions.Count);
        }
    }
}