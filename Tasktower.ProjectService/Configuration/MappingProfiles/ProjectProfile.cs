using AutoMapper;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectSaveDto, ProjectEntity>();
            CreateMap<ProjectEntity, ProjectReadDto>();
            CreateMap<ProjectReadDto, ProjectEntity>();
        }
        
    }
}