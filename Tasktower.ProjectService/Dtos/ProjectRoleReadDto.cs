using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectRoleReadDto : AuditableReadDto
    {
        public Guid Id { get; set; }
        
        public string UserId { get; set; }
        
        public ProjectRoleValue Role { get; set; }
        
        public bool PendingInvite { get; set; }
        
    }
}
