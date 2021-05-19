using AutoMapper;
using Tasktower.ProjectService.DataAccess.Entities;
using Tasktower.ProjectService.DataAccess.Entities.Base;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Profiles
{
    public class AuditableProfile : Profile
    {
        public AuditableProfile()
        {
            CreateMap<AuditableEntity, AuditableDto>();
            CreateMap<AuditableDto, AuditableEntity>();
        }
    }
}