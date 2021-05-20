using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectRoleDto : AuditableDto
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }
        
        public ProjectRoleValue Role { get; set; }
        
        public Guid ProjectId { get; set; }
        
        public bool PendingInvite { get; set; }
        
        public virtual ProjectReadDto Project { get; set; }
        
    }
}
