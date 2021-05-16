using System.Collections.Generic;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.BusinessLogic
{
    public interface IErrorService
    {
        public AppException Create(ErrorCode errorCode);
        
        public AppException CreateFromMultiple(IEnumerable<AppException> apiExceptions);
    }
}