using System.Threading.Tasks;
using Tasktower.BoardService.DataAccess.Context;
using Tasktower.BoardService.Tools.DependencyInjection;

namespace Tasktower.BoardService.DataAccess.Repositories.Impl
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
