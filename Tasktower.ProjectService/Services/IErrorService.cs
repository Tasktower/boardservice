using System.Collections.Generic;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Services
{
    public interface IErrorService
    {
        public AppException Create(ErrorCode errorCode, params object[] args);
        
        public AppException CreateFromMultiple(IEnumerable<AppException> apiExceptions, params object[] args);
    }
}