using AutoMapper;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Profiles
{
    public class ProjectRoleProfile: Profile
    {
        public ProjectRoleProfile()
        {
            CreateMap<ProjectRoleEntity, ProjectRoleDto>();
            CreateMap<ProjectRoleDto, ProjectRoleEntity>();
        }

    }
}