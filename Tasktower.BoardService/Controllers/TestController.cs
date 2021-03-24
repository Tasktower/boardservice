using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Tasktower.BoardService.Data.Context;
using Tasktower.BoardService.Data.DAL;
using Tasktower.BoardService.Data.Entities;
using Tasktower.BoardService.Security;

namespace Tasktower.BoardService.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly BoardDBContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public TestController(ILogger<TestController> logger, 
            BoardDBContext context, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("boards")]
        public async Task<IEnumerable<TaskBoard>> GetBoards()
        {
            var list = await _context.TaskBoards
                .Include(t => t.TaskBoardColumns)
                .Include("TaskBoardColumns.TaskCards")
                .Include(t => t.UserBoardRole)
                .ToListAsync();
            return list;
        }

        [HttpPost("insertColumn")]
        public async Task<TaskBoardColumn> InsertColumn(
            [FromBody]TaskBoardColumn boardColumn)
        {
            await _unitOfWork.TaskBoardColumnRepository.Insert(boardColumn);
            await _unitOfWork.SaveChanges();
            return boardColumn;
        }

        [HttpPost("updateColumn")]
        public async Task<TaskBoardColumn> UpdateColumn(
            [FromBody] TaskBoardColumn boardColumn)
        {
            await _unitOfWork.TaskBoardColumnRepository.Update(boardColumn);
            await _unitOfWork.SaveChanges();
            return boardColumn;
        }

        [HttpDelete("removeColumn/{id}")]
        public async Task<ActionResult> RemoveColumn(Guid id)
        {
            await _unitOfWork.TaskBoardColumnRepository.Delete(id);
            await _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpGet("userboardroles")]
        public async Task<UserTaskBoardRole> GetUserBoardRoles(
            [FromQuery(Name ="taskBoardId")] Guid taskBoardId, 
            [FromQuery(Name = "userid")] string userid)
        {
            return await _context.UserBoardRoles.FindAsync(taskBoardId, userid);
        }

        [Authorize]
        [HttpGet("auth")]
        public object GetData()
        {
            var userContext = new UserContext(User);
            return userContext;
        }

        [Authorize(Policy = Policies.PolicyNames.CanModerate)]
        [HttpGet("moderator")]
        public object GetData2()
        {
            var userContext = new UserContext(User);
            return userContext;
        }

        [Authorize(Policy = Policies.PolicyNames.Admin)]
        [HttpGet("adminonly")]
        public object GetData3()
        {
            var userContext = new UserContext(User);
            return userContext;
        }
    }
}
