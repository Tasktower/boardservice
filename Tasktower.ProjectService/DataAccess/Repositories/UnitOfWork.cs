using System.Threading.Tasks;
using Tasktower.ProjectService.DataAccess.Context;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoardDBContext _boardDbContext;
        
        public UnitOfWork(BoardDBContext boardDbContext,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            ITaskBoardRepository taskBoardRepository,
            ITaskRepository taskRepository,
            IProjectRoleRepository projectRoleRepository) 
        {
            _boardDbContext = boardDbContext;
            UserRepository = userRepository;
            ProjectRepository = projectRepository;
            TaskBoardRepository = taskBoardRepository;
            TaskRepository = taskRepository;
            ProjectRoleRepository = projectRoleRepository;
        }

        public async Task SaveChanges()
        {
            await _boardDbContext.SaveChangesAsync();
        }

        public IUserRepository UserRepository { get; }
        public IProjectRepository ProjectRepository { get;  }
        public ITaskBoardRepository TaskBoardRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public Repositories.IProjectRoleRepository ProjectRoleRepository { get; }
    }
}
