using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.Entities;

namespace Tasktower.BoardService.Dto
{
    public class TaskCardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaskDescriptionMarkup { get; set; }
        public Guid BoardColumnId { get; set; }
        public virtual TaskBoardColumnDto TaskBoardColumn { get; set; }

        public static TaskCardDto FromTaskCardDto(TaskCard taskCard,
            params Expression<Func<TaskCardDto, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskCard, TaskCardDto>.NewConfig()
                .Ignore(members)
                .Config;
            return taskCard.Adapt<TaskCardDto>(config);
        }

        public TaskCard ToTaskCard(params Expression<Func<TaskCard, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskCardDto, TaskCard>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<TaskCard>(config);
        }
    }
}
