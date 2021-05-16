using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Tasktower.ProjectService.Configuration;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.BusinessLogic
{
    public class ErrorService : IErrorService
    {
        private readonly ErrorConfiguration _errorConfiguration;
        
        public ErrorService(IOptions<ErrorConfiguration> errorConfigurationOptions)
        {
            _errorConfiguration = errorConfigurationOptions.Value;
        }

        public AppException Create(ErrorCode errorCode)
        {
            ErrorData errorData = _errorConfiguration.ErrorCodeMappings[errorCode.ToString()];
            return new AppException(errorCode, errorData, null);
        }
        
        public AppException CreateFromMultiple(IEnumerable<AppException> appExceptions)
        {
            ErrorData errorData = _errorConfiguration.ErrorCodeMappings[ErrorCode.MULTIPLE_ERRORS.ToString()];
            return new AppException(ErrorCode.MULTIPLE_ERRORS, errorData, appExceptions);
        }
    }
}