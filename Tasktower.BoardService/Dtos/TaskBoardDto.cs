using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mapster;
using Tasktower.BoardService.DataAccess.Entities;

namespace Tasktower.BoardService.Dtos
{
    public class TaskBoardDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<UserTaskBoardRoleDto> UserBoardRole { get; set; }

        public ICollection<UserTaskBoardRoleDto> TaskBoardColumns { get; set; }

        public static TaskBoardDto FromTaskCardDto(TaskBoard taskBoard,
            params Expression<Func<TaskBoardDto, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskBoard, TaskBoardDto>.NewConfig()
                .Ignore(members)
                .Config;
            return taskBoard.Adapt<TaskBoardDto>(config);
        }

        public TaskBoard ToTaskCard(params Expression<Func<TaskBoard, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskBoardDto, TaskBoard>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<TaskBoard>(config);
        }
    }
}
