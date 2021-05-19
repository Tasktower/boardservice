using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectRoleDto : AuditableDto
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }
        
        public ProjectRoleEntity.ProjectRoleValue Role { get; set; }
        
        public Guid ProjectId { get; set; }
        
        public virtual ProjectDto Project { get; set; }
        
    }
}
