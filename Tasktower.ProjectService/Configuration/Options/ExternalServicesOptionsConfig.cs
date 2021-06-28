using Tasktower.Lib.Aspnetcore.Attributes;

namespace Tasktower.ProjectService.Configuration.Options
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