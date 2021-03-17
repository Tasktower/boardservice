using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.DAL.Base;
using Tasktower.BoardService.Data.Entities;

namespace Tasktower.BoardService.Data.DAL
{
    public interface ITaskBoardColumnRepository : ICrudRepository<TaskBoardColumn>
    {
    }
}
