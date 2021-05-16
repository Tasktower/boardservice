using System;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class TaskBoardRepository : 
        CrudRepositoryImpl<Guid, TaskBoard, BoardDBContext>, 
        ITaskBoardRepository
    {
        public TaskBoardRepository(BoardDBContext context) : base(context) { }
    }
}
