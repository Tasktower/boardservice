using System;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    public class ProjectRepository : 
        CrudRepositoryImpl<Guid, Project, BoardDBContext>, 
        IProjectRepository
    {
        public ProjectRepository(BoardDBContext context) : base(context) { }
    }
}
