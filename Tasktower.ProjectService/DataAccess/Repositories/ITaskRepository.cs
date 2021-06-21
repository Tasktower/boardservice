using System;
using Tasktower.Lib.Aspnetcore.DataAccess.Repositories;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public interface ITaskRepository : ICrudRepository<Guid, TaskEntity>
    {
    }
}
