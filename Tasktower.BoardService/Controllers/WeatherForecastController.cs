using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tasktower.BoardService.Data.Context;
using Tasktower.BoardService.Data.Entities;
using Tasktower.BoardService.Security;
using Tasktower.Webtools.Security.Auth;

namespace Tasktower.BoardService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly BoardContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, BoardContext context)
        {
            _logger = logger;
            _context = context;
        }

       
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/boards")]
        public async Task<IEnumerable<TaskBoard>> GetBoards()
        {
            var list = await _context.TaskBoards
                .Include(t => t.BoardColumns)
                .Include(t => t.TaskCards)
                .Include(t => t.UserBoardRole)
                .ToListAsync();
            return list;
            //return list.Select(t =>
            //{
            //    t.BoardColumns = t.BoardColumns.Select(t =>
            //    {
            //        t.TaskBoard = null;
            //        return t;
            //    }).ToList();

            //    t.TaskCards = t.TaskCards.Select(t =>
            //    {
            //        t.TaskBoard = null;
            //        t.BoardColumn = null;
            //        return t;
            //    }).ToList();

            //    t.UserBoardRole = t.UserBoardRole.Select(t =>
            //    {
            //        t.TaskBoard = null;
            //        return t;
            //    }).ToList();

            //    return t;
            //});
        }

        [Authorize]
        [HttpGet("auth")]
        public object GetData()
        {
            var userContext = new UserContext(User);
            return userContext;
        }

        [Authorize(Policy = Policies.PolicyNameCanModerate)]
        [HttpGet("moderator")]
        public object GetData2()
        {
            var userContext = new UserContext(User);
            return userContext;
        }

        [Authorize(Policy = Policies.PolicyNameAdministrator)]
        [HttpGet("adminonly")]
        public object GetData3()
        {
            var userContext = new UserContext(User);
            return userContext;
        }
    }
}
