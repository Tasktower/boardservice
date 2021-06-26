using Tasktower.Lib.Aspnetcore.Attributes;

namespace Tasktower.ProjectService.Configuration
{
    [OptionsConfig("ExternalServices")]
    public class ExternalServicesOptionsConfig
    {
        public ExtServiceInfo UserService { get; set; }
        public class ExtServiceInfo
        {
            public string Url { get; set; }
        }
    }
}