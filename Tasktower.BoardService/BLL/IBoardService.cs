using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.BoardService.Dto;
using Tasktower.Webtools.Paging;

namespace Tasktower.BoardService.BLL
{
    public interface IBoardService
    {
        Page<TaskBoardDto> GetTaskBoards(String userId, String searchTitle);

    }
}
