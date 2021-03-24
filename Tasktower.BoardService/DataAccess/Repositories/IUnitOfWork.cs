using System.Threading.Tasks;

namespace Tasktower.BoardService.DataAccess.Repositories
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
