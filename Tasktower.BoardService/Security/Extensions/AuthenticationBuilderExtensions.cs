using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Security.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddHeaderAuthentication(this AuthenticationBuilder authenticationBuilder, Action<HeaderAuthenticationOptions> options)
        {
            return authenticationBuilder.AddScheme<HeaderAuthenticationOptions, HeaderAuthenticationHandler>(HeaderAuthenticationOptions.DefaultScheme, options);
        }
    }
}
