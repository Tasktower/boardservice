using System;
using System.Linq.Expressions;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class TaskDto : AuditableDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string TaskDescriptionMarkup { get; set; }
        
        public string Column { get; set; }
        
        public Guid TaskBoardId { get; set; }
        
        public virtual TaskBoardDto TaskBoard { get; set; }
    }
}
