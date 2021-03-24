using Tasktower.BoardService.DataAccess.Context;
using Tasktower.BoardService.DataAccess.Entities;
using Tasktower.BoardService.DataAccess.Repositories.Base;
using Tasktower.BoardService.Tools.DependencyInjection;

namespace Tasktower.BoardService.DataAccess.Repositories.Impl
{
    [ScopedService]
    public class TaskCardRepository : CrudRepositoryImpl<TaskCard, BoardDBContext>, ITaskCardRepository
    {
        public TaskCardRepository(BoardDBContext context) : base(context) { }
    }
}
