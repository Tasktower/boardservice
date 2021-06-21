using System;
using System.Collections.Generic;
using Tasktower.Lib.Aspnetcore.Dtos;

namespace Tasktower.ProjectService.Dtos
{
    public class TaskBoardReadDto : AuditableReadDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<string> Columns { get; set; }

    }
}
