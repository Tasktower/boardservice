using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.Context;
using Tasktower.BoardService.Data.DAL.BaseDAL;
using Tasktower.BoardService.Data.Entities;
using Tasktower.BoardService.Helpers.DependencyInjection;

namespace Tasktower.BoardService.Data.DAL.Impl
{
    [ScopedService]
    public class TaskBoardColumnRepository : BaseCrudEFCoreRepository<TaskBoardColumn, BoardDBContext>, ITaskBoardColumnRepository
    {
        public TaskBoardColumnRepository(BoardDBContext context) : base(context) { }
    }
}
