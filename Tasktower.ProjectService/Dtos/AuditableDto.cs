using System;
using System.Linq.Expressions;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class AuditableDto
    {
        public string CreatedBy { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string ModifiedBy { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}