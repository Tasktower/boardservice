using System.ComponentModel.DataAnnotations;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectSaveDto : AuditableSaveDto
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}