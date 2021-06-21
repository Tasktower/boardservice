using System;
using Tasktower.Lib.Aspnetcore.Dtos;

namespace Tasktower.ProjectService.Dtos
{
    public class TaskReadDto : AuditableReadDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string TaskDescriptionMarkup { get; set; }
        
        public string Column { get; set; }
    }
}
