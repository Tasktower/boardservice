using System;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class ProjectRoleRepository : 
        CrudRepositoryImpl<Guid, ProjectRole, BoardDBContext>, 
        Repositories.IProjectRoleRepository
    {
        public ProjectRoleRepository(BoardDBContext context) : base(context) { }
    }
}
