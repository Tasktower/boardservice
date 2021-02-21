using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.Entities;
using Tasktower.Webtools.Datatools.BaseDAL;

namespace Tasktower.BoardService.Data.DAL
{
    public interface ITaskCardRepository : ICrudRepository<TaskCard>
    {
    }
}
