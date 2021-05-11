using System;
using Tasktower.BoardService.DataAccess.Context;
using Tasktower.BoardService.DataAccess.Entities;
using Tasktower.BoardService.DataAccess.Repositories.Base;
using Tasktower.BoardService.Tools.DependencyInjection;

namespace Tasktower.BoardService.DataAccess.Repositories.Impl
{
    [ScopedService]
    public class TaskRepository : 
        CrudRepositoryImpl<Guid, Task, BoardDBContext>, 
        ITaskRepository
    {
        public TaskRepository(BoardDBContext context) : base(context) { }
    }
}
