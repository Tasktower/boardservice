using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class ExtUserPublicReadDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
    }
}