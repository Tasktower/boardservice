using System;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories.Base;
using Tasktower.ProjectService.Tools.DependencyInjection;

namespace Tasktower.ProjectService.DataAccess.Repositories.Impl
{
    [ScopedService]
    public class ProjectRepository : 
        CrudRepositoryImpl<Guid, Project, BoardDBContext>, 
        IProjectRepository
    {
        public ProjectRepository(BoardDBContext context) : base(context) { }
    }
}
