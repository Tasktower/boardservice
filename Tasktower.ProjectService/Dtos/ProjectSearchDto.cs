using System;
using StackExchange.Redis;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectSearchDto
    {
        public string ProjectOwner { get; set; }
        public ProjectReadDto Project { get; set; }
    }
}