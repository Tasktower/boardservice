namespace Tasktower.ProjectService.BusinessLogic
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