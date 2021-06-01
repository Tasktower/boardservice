using System;
using System.Collections.Generic;

namespace Tasktower.ProjectService.Dtos
{
    public class ProjectDetailsDto
    {
        public ProjectReadDto Project { get; set; }
        
        public List<ProjectRoleReadDto> ProjectRoles { get; set; }

        public List<TaskBoardReadDto> TaskBoards { get; set; }
    }
}