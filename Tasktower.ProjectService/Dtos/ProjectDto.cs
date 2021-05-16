using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mapster;
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

        public static ProjectDto FromProject(Project project,
            params Expression<Func<ProjectDto, object>>[] members)
        {
            var config = TypeAdapterConfig<Project, ProjectDto>.NewConfig()
                .Ignore(members)
                .PreserveReference(true)
                .Config;
            return project.Adapt<ProjectDto>(config);
        }

        public Project ToTaskProject(params Expression<Func<Project, object>>[] members)
        {
            var config = TypeAdapterConfig<ProjectDto, Project>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<Project>(config);
        }
    }
}
