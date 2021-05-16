using System;
using System.Linq.Expressions;
using Mapster;
using Tasktower.ProjectService.DataAccess.Entities;

namespace Tasktower.ProjectService.Dtos
{
    public class AuditableDto
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        
        public static AuditableDto FromAuditableEntity(BaseAuditableEntity project,
            params Expression<Func<AuditableDto, object>>[] members)
        {
            var config = TypeAdapterConfig<BaseAuditableEntity, AuditableDto>.NewConfig()
                .Ignore(members)
                .PreserveReference(true)
                .Config;
            return project.Adapt<AuditableDto>(config);
        }

        public Project ToAuditableEntity(params Expression<Func<BaseAuditableEntity, object>>[] members)
        {
            var config = TypeAdapterConfig<AuditableDto, BaseAuditableEntity>.NewConfig()
                .Ignore(members)
                .Config;
            return this.Adapt<Project>(config);
        }
    }
}