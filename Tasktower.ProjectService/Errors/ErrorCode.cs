namespace Tasktower.ProjectService.Errors
{
    public enum ErrorCode
    {
        OPTIMISTIC_LOCKING,
        NO_PROJECT_PERMISSIONS,
        PROJECT_ID_NOT_FOUND,
        NON_EXISTENT_COLUMN,
        MULTIPLE_ERRORS
    }
}