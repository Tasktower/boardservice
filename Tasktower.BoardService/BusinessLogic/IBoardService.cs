using System;
using Tasktower.BoardService.Dtos;
using Tasktower.BoardService.Tools.Paging;

namespace Tasktower.BoardService.BusinessLogic
{
    public interface IBoardService
    {
        Page<ProjectDto> GetTaskBoards(String userId, String searchTitle);

    }
}
