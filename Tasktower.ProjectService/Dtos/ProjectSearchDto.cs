using System;
using StackExchange.Redis;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectSearchDto
    {
        public string ProjectOwnerId { get; set; }
        public ProjectReadDto Project { get; set; }
    }
}