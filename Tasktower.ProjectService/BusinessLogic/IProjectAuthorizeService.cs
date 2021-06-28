using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.BusinessLogic
{
    public interface IProjectAuthorizeService
    {
        public ValueTask Authorize(Guid projectId, ISet<ProjectRoleValue> projectRoles, 
            bool allowPendingInvite = false);

        public ISet<ProjectRoleValue> OwnerRoles();
        public ISet<ProjectRoleValue> WriterRoles();
        public ISet<ProjectRoleValue> ReaderRoles();
    }
}