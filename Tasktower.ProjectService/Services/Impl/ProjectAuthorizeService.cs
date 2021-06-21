using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.Lib.Aspnetcore.Security;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Services.Impl
{
    public class ProjectAuthorizeService : IProjectAuthorizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorService _errorService;
        private readonly IUserContext _userContext;
        
        public ProjectAuthorizeService(IUnitOfWork unitOfWork, IErrorService errorService, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _errorService = errorService;
            _userContext = userContext;
        }


        public async ValueTask Authorize(Guid projectId, ISet<ProjectRoleValue> projectRoles, 
            bool allowPendingInvite = false)
        {
            var hasPermission = await _unitOfWork.ProjectRoleRepository.UserHasProjectRolePermission(
                projectId, _userContext.UserId, projectRoles, allowPendingInvite);
            if (!hasPermission)
            {
                throw _errorService.Create(ErrorCode.NoProjectPermissions, projectId);
            }
        }

        public ISet<ProjectRoleValue> OwnerRoles()
        {
            return new HashSet<ProjectRoleValue>()
            {
                ProjectRoleValue.OWNER
            };
        }

        public ISet<ProjectRoleValue> WriterRoles()
        {
            return new HashSet<ProjectRoleValue>()
            {
                ProjectRoleValue.OWNER, 
                ProjectRoleValue.PROJECT_WRITER
            };
        }

        public ISet<ProjectRoleValue> ReaderRoles()
        {
            return new HashSet<ProjectRoleValue>()
            {
                ProjectRoleValue.OWNER, 
                ProjectRoleValue.PROJECT_WRITER, 
                ProjectRoleValue.PROJECT_READER
            };
        }
    }
}