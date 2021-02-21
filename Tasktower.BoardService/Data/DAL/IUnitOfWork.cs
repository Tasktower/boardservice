using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Data.DAL
{
    public interface IUnitOfWork
    {
        public ValueTask SaveChanges();

        public ITaskBoardRepository TaskBoardRepository { get; }
        
        public ITaskBoardColumnRepository TaskBoardColumnRepository { get;  }

        public ITaskCardRepository TaskCardRepository { get; }

        public IUserTaskBoardRoleRepository UserTaskBoardRoleRepository { get; }
    }
}
