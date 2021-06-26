using System;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectSearchDto
    {
        public UserReadDto Owner { get; set; }
        public ProjectReadDto Project { get; set; }
    }
}