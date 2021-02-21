using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Data.Entities
{
    public class TaskCard : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaskDescriptionMarkup { get; set; }
        public Guid BoardColumnId { get; set; }
        public virtual TaskBoardColumn TaskBoardColumn { get; set; }
    }
}
