using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tasktower.BoardService.Data.Entities;
using static Tasktower.BoardService.Data.Entities.UserTaskBoardRole;

namespace Tasktower.BoardService.Dto
{
    public class UserTaskBoardRoleDto
    {
        public Guid TaskBoardId { get; set; }

        public virtual TaskBoardDto TaskBoard { get; set; }

        public string UserId { get; set; }

        public BoardRole Role { get; set; }

        public static UserTaskBoardRoleDto FromUserTaskBoardRole(UserTaskBoardRole userTaskBoardRole, 
            params Expression<Func<UserTaskBoardRoleDto, object>>[] members)
        {
            var config = TypeAdapterConfig<UserTaskBoardRole, UserTaskBoardRoleDto>.NewConfig()
                .Ignore(members)
                .Config;
            return userTaskBoardRole.Adapt<UserTaskBoardRoleDto>(config);
        }

        public UserTaskBoardRole ToUserTaskBoardRole(params Expression<Func<UserTaskBoardRole, object>>[] members)
        {
            var config = TypeAdapterConfig<UserTaskBoardRoleDto, UserTaskBoardRole>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<UserTaskBoardRole>(config);
        }
    }
}
