using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tasktower.BoardService.Security;

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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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

        [Authorize]
        [HttpGet("auth")]
        public string GetData()
        {
            var user = this.User;
            var id = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            var roles = user.FindAll(System.Security.Claims.ClaimTypes.Role);
            return $"{id}, {string.Join(",", from role in roles select role.Value)}";
        }

        [Authorize(Policy = Policies.PolicyNameCanModerate)]
        [HttpGet("moderator")]
        public string GetData2()
        {
            var user = this.User;
            var id = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            var roles = user.FindAll(System.Security.Claims.ClaimTypes.Role);
            return $"{id}, {string.Join(",", from role in roles select role.Value)}";
        }

        [Authorize(Policy = Policies.PolicyNameAdministrator)]
        [HttpGet("adminonly")]
        public string GetData3()
        {
            var user = this.User;
            var id = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            var roles = user.FindAll(System.Security.Claims.ClaimTypes.Role);
            return $"{id}, {string.Join(",", from role in roles select role.Value)}";
        }
    }
}
