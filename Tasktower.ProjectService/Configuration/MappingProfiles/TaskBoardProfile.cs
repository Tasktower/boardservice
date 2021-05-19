using AutoMapper;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Profiles
{
    public class TaskBoardProfile : Profile
    {
        public TaskBoardProfile()
        {
            CreateMap<TaskBoardEntity, TaskBoardDto>();
            CreateMap<TaskBoardDto, TaskBoardEntity>();
        }

    }
}