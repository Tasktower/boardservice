using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Data.Entities
{
    public class UserBoardRole : AbstractAuditableEntity
    {
        public enum BoardRole { OWNER, BOARD_EDITOR, BOARD_READER }
        public Guid TaskBoardId { get; set; }

        public virtual TaskBoard TaskBoard { get; set; }

        public string UserId { get; set; }

        public BoardRole Role { get; set; }

    }
}
