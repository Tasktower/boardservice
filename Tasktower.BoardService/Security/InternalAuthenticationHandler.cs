using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Security
{
    public class InternalAuthenticationHandler : AuthenticationHandler<InternalAuthenticationOptions>
    {
        private const string ProblemDetailsContentType = "application/problem+json";
        private const string UserIdHeaderName = "userid";
        private const string NameHeaderName = "name";
        private const string EmailHeaderName = "email";
        private const string RolesHeaderName = "roles";
        public InternalAuthenticationHandler(
            IOptionsMonitor<InternalAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
 
            if (!Request.Headers.TryGetValue(UserIdHeaderName, out var userIdHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }
            var userId = userIdHeaderValues.FirstOrDefault();
            if (userIdHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(userId))
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.TryGetValue(NameHeaderName, out var namedHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }
            var name = namedHeaderValues.FirstOrDefault();
            if (namedHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(name))
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.TryGetValue(EmailHeaderName, out var emailHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }
            var email = emailHeaderValues.FirstOrDefault();
            if (emailHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(email))
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.TryGetValue(RolesHeaderName, out var roleHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }
            var roles = roleHeaderValues.SelectMany(x => x.Split(",")).Where(x => !string.IsNullOrWhiteSpace(x));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal(identities);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);

            return AuthenticateResult.Success(ticket);
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new { problem = "unauthenticated" };

            await Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 403;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new { problem = "unauthorized" };

            await Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
