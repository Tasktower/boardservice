using System.Collections.Generic;
using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Services
{
    public interface IErrorService : IBaseErrorService<ErrorCode>
    {
    }
}