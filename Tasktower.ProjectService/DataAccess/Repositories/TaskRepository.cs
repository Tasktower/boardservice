using System;
using Tasktower.Lib.Aspnetcore.DataAccess.Repositories;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public class TaskRepository : 
        CrudRepositoryEfCore<Guid, TaskEntity, BoardDBContext>, 
        ITaskRepository
    {
        public TaskRepository(BoardDBContext context) : base(context) { }
    }
}
