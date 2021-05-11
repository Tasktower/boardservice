using System.Threading.Tasks;
using Tasktower.BoardService.DataAccess.Context;
using Tasktower.BoardService.Tools.DependencyInjection;

namespace Tasktower.BoardService.DataAccess.Repositories.Impl
{
    [ScopedService]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoardDBContext _boardDbContext;
        
        public UnitOfWork(BoardDBContext boardDbContext,
            IProjectRepository projectRepository,
            ITaskBoardRepository taskBoardRepository,
            ITaskRepository taskRepository,
            Repositories.IProjectRoleRepository projectRoleRepository) 
        {
            _boardDbContext = boardDbContext;
            ProjectRepository = projectRepository;
            TaskBoardRepository = taskBoardRepository;
            TaskRepository = taskRepository;
            ProjectRoleRepository = projectRoleRepository;
        }

        public async ValueTask SaveChanges()
        {
            await _boardDbContext.SaveChangesAsync();
        }

        public IProjectRepository ProjectRepository { get;  }
        public ITaskBoardRepository TaskBoardRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public Repositories.IProjectRoleRepository ProjectRoleRepository { get; }
    }
}
