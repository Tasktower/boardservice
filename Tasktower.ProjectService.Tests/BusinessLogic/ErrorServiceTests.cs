﻿using System;
using System.Net;
using Tasktower.ProjectService.BusinessLogic;
using Tasktower.ProjectService.Errors;
using Xunit;

namespace Tasktower.ProjectService.Tests.BusinessLogic
{
    public class ErrorServiceTests
    {
        private readonly IErrorService _errorService;

        public ErrorServiceTests(IErrorService errorService)
        {
            _errorService = errorService;
        }

        [Fact]
        public void CreateError_OptimisticLocking_ReturnOptimisticLockingFromConfig()
        {
            var appError = _errorService.Create(ErrorCode.OptimisticLocking);
            Assert.Equal(HttpStatusCode.Conflict, appError.StatusCode);
            Assert.Equal("Optimistic locking", appError.Message);
        }
        
        [Fact]
        public void CreateError_ProjectIdNotFound_ReturnProjectIdNotFoundFromConfig()
        {
            var randomId = Guid.NewGuid();
            var appError = _errorService.Create(ErrorCode.ProjectIdNotFound, randomId);
            Assert.Equal(HttpStatusCode.NotFound, appError.StatusCode);
            Assert.Equal($"Project with id {randomId.ToString()} not found", appError.Message);
        }
    }
}