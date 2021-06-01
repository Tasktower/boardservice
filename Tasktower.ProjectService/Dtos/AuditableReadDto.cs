using System;

namespace Tasktower.ProjectService.Dtos
{
    public class AuditableReadDto
    {
        public long Version { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string ModifiedBy { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}