﻿using System.Threading.Tasks;

namespace Tasktower.ProjectService.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        public ValueTask SaveChanges();

        public IProjectRepository ProjectRepository { get; }
        
        public ITaskBoardRepository TaskBoardRepository { get;  }

        public ITaskRepository TaskRepository { get; }

        public IProjectRoleRepository ProjectRoleRepository { get; }
    }
}