using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestClient.Net;
using Tasktower.Lib.Aspnetcore.Tools.Http;
using Tasktower.ProjectService.Configuration;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;
using Urls;

namespace Tasktower.ProjectService.Services.External
{
    public class ExternalUserService : IExternalUserService
    {
        private readonly IClient _client;
        private readonly ExternalServicesOptionsConfig _externalServicesOptionsConfig;
        private readonly IErrorService _errorService;
        
        public ExternalUserService(IOptions<ExternalServicesOptionsConfig>  externalServicesOptions, 
            CreateClient createClient, IErrorService errorService)
        {
            _externalServicesOptionsConfig = externalServicesOptions.Value;
            _client = _client = createClient("Oath2Client", o =>
            {
                o.BaseUrl = _externalServicesOptionsConfig.UserService.Url.ToAbsoluteUrl();
                o.DefaultRequestHeaders = HeaderCollectionUtils.JsonHeadersDefault;
            });
        }
        
        public async Task<ExtUserPublicReadDto> GetUser(string UserId)
        {
            var res = await _client.GetAsync<ExtUserPublicReadDto>($"/users/public/{UserId}");
            if (res.IsSuccess) return res.Body;
            if (res.StatusCode == (int) HttpStatusCode.NotFound)
            {
                throw _errorService.Create(ErrorCode.UserNotFound);
            }
            throw new HttpRequestException($"Bad request for {UserId}");
        }
    }
}