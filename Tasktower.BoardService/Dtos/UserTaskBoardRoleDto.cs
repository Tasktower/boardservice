using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Mapster;
using Tasktower.BoardService.DataAccess.Entities;
using static Tasktower.BoardService.DataAccess.Entities.UserTaskBoardRole;

namespace Tasktower.BoardService.Dtos
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
