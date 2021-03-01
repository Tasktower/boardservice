using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Dto
{
    public class TaskBoardDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public ICollection<UserTaskBoardRole> UserBoardRole { get; set; }

        //public ICollection<TaskBoardColumn> TaskBoardColumns { get; set; }
    }
}
