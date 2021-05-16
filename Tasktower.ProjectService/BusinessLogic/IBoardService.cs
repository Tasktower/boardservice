using System;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Tools.Paging;

namespace Tasktower.ProjectService.BusinessLogic
{
    public interface IBoardService
    {
        Page<ProjectDto> GetTaskBoards(String userId, String searchTitle);

    }
}
