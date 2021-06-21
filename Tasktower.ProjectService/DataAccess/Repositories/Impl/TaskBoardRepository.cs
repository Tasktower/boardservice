using System;
using Tasktower.Lib.Aspnetcore.DataAccess.Repositories;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class TaskBoardRepository : 
        CrudRepositoryEfCore<Guid, TaskBoardEntity, BoardDBContext>, 
        ITaskBoardRepository
    {
        public TaskBoardRepository(BoardDBContext context) : base(context) { }
    }
}
