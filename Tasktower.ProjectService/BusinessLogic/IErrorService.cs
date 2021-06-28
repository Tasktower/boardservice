using Tasktower.Lib.Aspnetcore.Services;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.BusinessLogic
{
    public interface IErrorService : IBaseErrorService<ErrorCode>
    {
    }
}