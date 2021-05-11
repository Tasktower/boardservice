using System;
using Tasktower.BoardService.DataAccess.Context;
using Tasktower.BoardService.DataAccess.Entities;
using Tasktower.BoardService.DataAccess.Repositories.Base;
using Tasktower.BoardService.Tools.DependencyInjection;

namespace Tasktower.BoardService.DataAccess.Repositories.Impl
{
    [ScopedService]
    public class ProjectRoleRepository : 
        CrudRepositoryImpl<Guid, ProjectRole, BoardDBContext>, 
        Repositories.IProjectRoleRepository
    {
        public ProjectRoleRepository(BoardDBContext context) : base(context) { }
    }
}
