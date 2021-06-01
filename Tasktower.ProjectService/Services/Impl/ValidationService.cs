using Tasktower.ProjectService.DataAccess.Entities.Base;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Services.Impl
{
    public class ValidationService : IValidationService
    {
        private readonly IErrorService _errorService;

        public ValidationService(IErrorService errorService)
        {
            _errorService = errorService;
        }
    }
}