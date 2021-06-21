using System;
using Tasktower.Lib.Aspnetcore.Dtos;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectReadDto : AuditableReadDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
    }
}
