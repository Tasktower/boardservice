using System;
using Tasktower.BoardService.DataAccess.Repositories;
using Tasktower.BoardService.Dtos;
using Tasktower.BoardService.Tools.DependencyInjection;
using Tasktower.BoardService.Tools.Paging;

namespace Tasktower.BoardService.BusinessLogic
{
    [ScopedService]
    public class BoardService : IBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BoardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Page<TaskBoardDto> GetTaskBoards(string userId, string searchTitle)
        {
            // Todo: impliment
            throw new NotImplementedException();
        }
    }
}
