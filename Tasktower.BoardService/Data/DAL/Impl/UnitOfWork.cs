using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.Context;
using Tasktower.Webtools.DependencyInjection;

namespace Tasktower.BoardService.Data.DAL.Impl
{
    [ScopedService]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoardDBContext _boardDBContext;
        public UnitOfWork(BoardDBContext boardDBContext,
            ITaskBoardRepository taskBoardRepository,
            ITaskBoardColumnRepository taskBoardColumnRepository,
            ITaskCardRepository taskCardRepository,
            IUserTaskBoardRoleRepository userTaskBoardRoleRepository) 
        {
            _boardDBContext = boardDBContext;
            TaskBoardRepository = taskBoardRepository;
            TaskBoardColumnRepository = taskBoardColumnRepository;
            TaskCardRepository = taskCardRepository;
            UserTaskBoardRoleRepository = userTaskBoardRoleRepository;
        }

        public async ValueTask SaveChanges()
        {
            await _boardDBContext.SaveChangesAsync();
        }

        public ITaskBoardRepository TaskBoardRepository { get;  }
        public ITaskBoardColumnRepository TaskBoardColumnRepository { get; }
        public ITaskCardRepository TaskCardRepository { get; }
        public IUserTaskBoardRoleRepository UserTaskBoardRoleRepository { get; }
    }
}
