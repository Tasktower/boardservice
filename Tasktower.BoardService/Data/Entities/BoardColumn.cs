using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Data.Entities
{
    public class BoardColumn : AbstractAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid TaskBoardId { get; set; }
        public string Name { get; set; }
        public virtual TaskBoard TaskBoard { get; set; }
        public ICollection<TaskCard> TaskCards { get; set; }
    }
}
