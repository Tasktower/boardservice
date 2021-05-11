﻿using System;
using Tasktower.BoardService.DataAccess.Entities;
using Tasktower.BoardService.DataAccess.Repositories.Base;

namespace Tasktower.BoardService.DataAccess.Repositories
{
    public interface ITaskBoardRepository : ICrudRepository<Guid, TaskBoard>
    {
    }
}
