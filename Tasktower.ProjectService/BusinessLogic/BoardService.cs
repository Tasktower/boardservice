using System;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Tools.DependencyInjection;
using Tasktower.ProjectService.Tools.Paging;

namespace Tasktower.ProjectService.BusinessLogic
{
    [ScopedService]
    public class BoardService : IBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BoardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Page<ProjectDto> GetTaskBoards(string userId, string searchTitle)
        {
            // Todo: impliment
            throw new NotImplementedException();
        }
    }
}
