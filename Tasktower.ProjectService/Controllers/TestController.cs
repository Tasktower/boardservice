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
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.DataAccess.Context;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Security;

namespace Tasktower.ProjectService.Controllers
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

        [HttpGet("project")]
        public async Task<IEnumerable<Project>> GetBoards()
        {
            var list = await _context.Projects
                .Include(t => t.TaskBoards)
                .Include("TaskBoards.Tasks")
                .Include(t => t.ProjectRoles)
                .ToListAsync();
            return list;
        }

        [HttpGet("projectRoles")]
        public async Task<ProjectRole> GetUserBoardRoles(
            [FromQuery(Name ="id")] Guid id)
        {
            return await _unitOfWork.ProjectRoleRepository.GetById(id);
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
