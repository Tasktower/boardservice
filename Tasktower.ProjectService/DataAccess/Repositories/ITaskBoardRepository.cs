using System;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public interface ITaskBoardRepository : ICrudRepository<Guid, TaskBoard>
    {
    }
}
