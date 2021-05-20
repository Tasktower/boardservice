using System.ComponentModel.DataAnnotations;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectSaveDto
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}