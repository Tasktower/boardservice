using System.Text.Json.Serialization;

namespace Tasktower.ProjectService.Dtos.External
{
    public class ExtUserSyncMessageDto
    {
        public string UserId { get; set; }

        public string UserIdCurrent() => UserId;

        public string UserIdToUpdateTo() => UserProfileToSync?.UserId;

        public ExtUserProfileReadDto UserProfileToSync;
    }
}