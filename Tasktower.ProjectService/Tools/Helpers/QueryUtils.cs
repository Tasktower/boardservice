namespace Tasktower.ProjectService.Tools.Helpers
{
    public static class QueryUtils
    {
        public static string LikeWrap(string s) => $"%{s}%";
    }
}