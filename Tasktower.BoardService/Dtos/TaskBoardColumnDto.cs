﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mapster;
using Tasktower.BoardService.DataAccess.Entities;

namespace Tasktower.BoardService.Dtos
{
    public class TaskBoardColumnDto
    {
        public Guid Id { get; set; }
        public Guid TaskBoardId { get; set; }
        public string Name { get; set; }
        public virtual TaskBoardDto TaskBoard { get; set; }
        public ICollection<TaskCardDto> TaskCards { get; set; }

        public static TaskBoardColumnDto FromTaskCardDto(TaskBoardColumn taskBoard,
            params Expression<Func<TaskBoardColumnDto, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskBoard, TaskBoardColumnDto>.NewConfig()
                .Ignore(members)
                .Config;
            return taskBoard.Adapt<TaskBoardColumnDto>(config);
        }

        public TaskBoardColumn ToTaskCard(params Expression<Func<TaskBoardColumn, object>>[] members)
        {
            var config = TypeAdapterConfig<TaskBoardColumnDto, TaskBoardColumn>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<TaskBoardColumn>(config);
        }
    }
}
