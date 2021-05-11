﻿using System;
using Tasktower.BoardService.DataAccess.Entities;
using Tasktower.BoardService.DataAccess.Repositories.Base;

namespace Tasktower.BoardService.DataAccess.Repositories
{
    public interface ITaskRepository : ICrudRepository<Guid, Task>
    {
    }
}
