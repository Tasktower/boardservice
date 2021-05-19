using System;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class TaskRepository : 
        CrudRepositoryImpl<Guid, TaskEntity, BoardDBContext>, 
        ITaskRepository
    {
        public TaskRepository(BoardDBContext context) : base(context) { }
    }
}
