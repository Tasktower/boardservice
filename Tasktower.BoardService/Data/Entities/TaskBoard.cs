using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Data.Entities
{
    public class TaskBoard : AbstractAuditableEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<UserBoardRole> UserBoardRole { get; set; }

        public ICollection<BoardColumn> BoardColumns { get; set; }

    }
}
