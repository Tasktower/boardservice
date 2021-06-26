using System.Threading.Tasks;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        public Task SaveChanges();
        
        public IUserRepository UserRepository { get; }

        public IProjectRepository ProjectRepository { get; }
        
        public ITaskBoardRepository TaskBoardRepository { get;  }

        public ITaskRepository TaskRepository { get; }

        public IProjectRoleRepository ProjectRoleRepository { get; }
    }
}
