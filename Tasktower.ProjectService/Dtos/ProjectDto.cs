using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectDto : AuditableDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<ProjectRoleDto> ProjectRoles { get; set; }
        
        public ICollection<TaskBoardDto> TaskBoards { get; set; }
        
    }
}
