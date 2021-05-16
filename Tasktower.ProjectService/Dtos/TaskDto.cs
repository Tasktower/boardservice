using System;
using System.Linq.Expressions;
using Mapster;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class TaskDto : AuditableDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaskDescriptionMarkup { get; set; }
        public string Column { get; set; }
        public Guid TaskBoardId { get; set; }
        public virtual TaskBoardDto TaskBoard { get; set; }

        public static TaskDto FromTask(Task task,
            params Expression<Func<TaskDto, object>>[] members)
        {
            var config = TypeAdapterConfig<Task, TaskDto>.NewConfig()
                .Ignore(members)
                .Config;
            return task.Adapt<TaskDto>(config);
        }

        public Task ToTask(params Expression<Func<Task, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskDto, Task>.NewConfig()
                .Ignore(members)
                .PreserveReference(true)
                .Config;
            return this.Adapt<Task>(config);
        }
    }
}
