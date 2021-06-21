namespace Tasktower.ProjectService.Errors
{
    public enum ErrorCode
    {
        MultipleErrors,
        OptimisticLocking,
        ProjectIdNotFound,
        NoProjectPermissions,
        NonExistentColumn
    }
}