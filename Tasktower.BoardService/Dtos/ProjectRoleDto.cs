using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Mapster;
using Tasktower.BoardService.DataAccess.Entities;

namespace Tasktower.BoardService.Dtos
{
    public class ProjectRoleDto : AuditableDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public ProjectRole.ProjectRoleValue Role { get; set; }
        
        public Guid ProjectId { get; set; }

        public virtual ProjectDto Project { get; set; }

        public static ProjectRoleDto FromProjectRole(ProjectRole projectRole, 
            params Expression<Func<ProjectRoleDto, object>>[] members)
        {
            var config = TypeAdapterConfig<ProjectRole, ProjectRoleDto>.NewConfig()
                .Ignore(members)
                .PreserveReference(true)
                .Config;
            return projectRole.Adapt<ProjectRoleDto>(config);
        }

        public ProjectRole ToProjectRole(params Expression<Func<ProjectRole, object>>[] members)
        {
            var config = TypeAdapterConfig<ProjectRoleDto, ProjectRole>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<ProjectRole>(config);
        }
    }
}
