using System.Collections.Generic;

namespace Tasktower.ProjectService.Dtos
{
    public class TaskBoardDetailsDto
    {
        public TaskBoardReadDto TaskBoard { get; set; }
        public Dictionary<string, IEnumerable<TaskReadDto>> ColumnsToTasks { get; set; }
    }
}