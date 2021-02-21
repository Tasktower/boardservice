using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Data.Entities
{
    public class TaskCard : AbstractAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaskDescriptionMarkup { get; set; }
        public Guid TaskBoardId { get; set; }
        public Guid BoardColumnId { get; set; }
        public virtual TaskBoard TaskBoard { get; set; }
        public virtual BoardColumn BoardColumn { get; set; }
    }
}
