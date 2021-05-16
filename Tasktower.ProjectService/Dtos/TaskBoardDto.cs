using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mapster;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class TaskBoardDto : AuditableDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Columns { get; set; }
        
        public Guid ProjectId { get; set; }
        public virtual ProjectDto Project { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }

        public static TaskBoardDto FromTaskBoard(TaskBoard taskBoard,
            params Expression<Func<TaskBoardDto, object>>[] members)
        {
            var config = TypeAdapterConfig<Project, TaskBoardDto>.NewConfig()
                .Ignore(members)
                .PreserveReference(true)
                .Config;
            return taskBoard.Adapt<TaskBoardDto>(config);
        }

        public TaskBoard ToTaskBoard(params Expression<Func<TaskBoard, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskBoardDto, TaskBoard>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<TaskBoard>(config);
        }
    }
}
