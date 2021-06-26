namespace Tasktower.ProjectService.Errors
{
    public enum ErrorCode
    {
        MultipleErrors,
        OptimisticLocking,
        UserNotFound,
        ProjectIdNotFound,
        NoProjectPermissions,
        NonExistentColumn
    }
}