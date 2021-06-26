using Tasktower.Lib.Aspnetcore.DataAccess.Repositories;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public class UserRepository : CrudRepositoryEfCore<string, UserEntity, BoardDBContext>, IUserRepository
    {
        public UserRepository(BoardDBContext context) : base(context) { }
    }
}