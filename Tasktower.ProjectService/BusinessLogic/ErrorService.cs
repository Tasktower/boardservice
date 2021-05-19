using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Tasktower.ProjectService.Configuration;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.BusinessLogic
{
    public class ErrorService : IErrorService
    {
        private readonly ErrorOptionsConfig _errorOptionsConfig;
        
        public ErrorService(IOptions<ErrorOptionsConfig> errorConfigurationOptions)
        {
            _errorOptionsConfig = errorConfigurationOptions.Value;
        }

        public AppException Create(ErrorCode errorCode)
        {
            var errorConfigData = _errorOptionsConfig.ErrorCodeMappings[errorCode.GetDisplayName()];
            return new AppException(errorCode, errorConfigData, null);
        }
        
        public AppException CreateFromMultiple(IEnumerable<AppException> appExceptions)
        {
            var errorConfigData = _errorOptionsConfig.ErrorCodeMappings[ErrorCode.MULTIPLE_ERRORS.ToString()];
            return new AppException(ErrorCode.MULTIPLE_ERRORS, errorConfigData, appExceptions);
        }
    }
}