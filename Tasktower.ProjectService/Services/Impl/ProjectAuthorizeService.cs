using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasktower.ProjectService.DataAccess.Repositories;
using Tasktower.ProjectService.Errors;
using Tasktower.ProjectService.Security;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Services.Impl
{
    public class ProjectAuthorizeService : IProjectAuthorizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorService _errorService;
        private readonly IUserContextService _userContextService;
        
        public ProjectAuthorizeService(IUnitOfWork unitOfWork, IErrorService errorService, 
            IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _errorService = errorService;
            _userContextService = userContextService;
        }


        public async ValueTask Authorize(Guid projectId,
            ISet<ProjectRoleValue> projectRoles)
        {
            var userContext = _userContextService.Get();
            var hasPermission = 
                await _unitOfWork.ProjectRoleRepository.UserHasProjectRolePermission(
                    projectId, userContext.UserId, projectRoles);
            if (!hasPermission || Roles.AdminGroup().Any(r => userContext.Roles.Contains(r)))
            {
                throw _errorService.Create(ErrorCode.NO_PROJECT_PERMISSIONS, projectId);
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