using Tasktower.BoardService.DataAccess.Context;
using Tasktower.BoardService.DataAccess.Entities;
using Tasktower.BoardService.DataAccess.Repositories.Base;
using Tasktower.BoardService.Tools.DependencyInjection;

namespace Tasktower.BoardService.DataAccess.Repositories.Impl
{
    [ScopedService]
    public class UserTaskBoardRoleRepository : CrudRepositoryImpl<UserTaskBoardRole, BoardDBContext>, IUserTaskBoardRoleRepository
    {
        public UserTaskBoardRoleRepository(BoardDBContext context) : base(context) { }
    }
}
