using System;

namespace Tasktower.ProjectService.Errors
{
    public class PaginationException : Exception
    {
        public PaginationException(string message) : base(message)
        {
            
        }
    }
}