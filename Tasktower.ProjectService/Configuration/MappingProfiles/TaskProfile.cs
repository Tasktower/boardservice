using AutoMapper;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskDto, TaskEntity>();
        }
    }
}