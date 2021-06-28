using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasktower.Lib.Aspnetcore.Services.Contexts;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.BusinessLogic
{
    public class ProjectAuthorizeService : IProjectAuthorizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorService _errorService;
        private readonly IUserContextAccessorService _userContextAccessorService;
        
        public ProjectAuthorizeService(IUnitOfWork unitOfWork, IErrorService errorService, IUserContextAccessorService userContextAccessorService)
        {
            _unitOfWork = unitOfWork;
            _errorService = errorService;
            _userContextAccessorService = userContextAccessorService;
        }


        public async ValueTask Authorize(Guid projectId, ISet<ProjectRoleValue> projectRoles, 
            bool allowPendingInvite = false)
        {
            var hasPermission = await _unitOfWork.ProjectRoleRepository.UserHasProjectRolePermission(
                projectId, _userContextAccessorService.UserContext.UserId, projectRoles, allowPendingInvite);
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