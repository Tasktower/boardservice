using System;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;
using Tasktower.ProjectService.Tools.DependencyInjection;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    [ScopedService]
    public class TaskRepository : 
        CrudRepositoryImpl<Guid, Task, BoardDBContext>, 
        ITaskRepository
    {
        public TaskRepository(BoardDBContext context) : base(context) { }
    }
}
