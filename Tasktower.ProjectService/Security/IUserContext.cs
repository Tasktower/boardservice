using System.Collections.Generic;

namespace Tasktower.ProjectService.Security
{
    public interface IUserContext
    {
        public bool IsAuthenticated { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public ICollection<string> Permissions { get; set; }
    }
}