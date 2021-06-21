using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Tasktower.Lib.Aspnetcore.Errors;
using Tasktower.Lib.Aspnetcore.Errors.Options;
using Tasktower.Lib.Aspnetcore.Errors.Services.Impl;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Services.Impl
{
    public class ErrorService : BaseBaseErrorService<ErrorCode>, IErrorService 
    {
        public ErrorService(IOptions<ErrorOptionsConfig> errorConfigurationOptions) : base(errorConfigurationOptions)
        {
        }

        public override AppException<ErrorCode> CreateFromMultiple(IEnumerable<AppException<ErrorCode>> apiExceptions, 
            params object[] args)
        {
            return base.CreateFromMultiple(ErrorCode.MultipleErrors, apiExceptions, args);
        }
    }
}