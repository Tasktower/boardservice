using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.DAL;
using Tasktower.BoardService.Dto;
using Tasktower.BoardService.Helpers.Paging;

namespace Tasktower.BoardService.BLL
{
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
