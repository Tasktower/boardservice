using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Tasktower.ProjectService.Configuration;
using Tasktower.ProjectService.Configuration.Options;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Services.Impl
{
    public class ErrorService : IErrorService
    {
        private readonly ErrorOptionsConfig _errorOptionsConfig;
        
        public ErrorService(IOptions<ErrorOptionsConfig> errorConfigurationOptions)
        {
            _errorOptionsConfig = errorConfigurationOptions.Value;
        }

        public AppException Create(ErrorCode errorCode, params object[] args)
        {
            var errorConfigData = _errorOptionsConfig.ErrorCodeMappings[errorCode.GetDisplayName()];
            return new AppException(errorCode, errorConfigData, null, args);
        }
        
        public AppException CreateFromMultiple(IEnumerable<AppException> appExceptions, params object[] args)
        {
            var errorConfigData = _errorOptionsConfig.ErrorCodeMappings[ErrorCode.MULTIPLE_ERRORS.GetDisplayName()];
            return new AppException(ErrorCode.MULTIPLE_ERRORS, errorConfigData, appExceptions, args);
        }
    }
}