using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Security.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddInternalAuthentication(this AuthenticationBuilder authenticationBuilder, Action<InternalAuthenticationOptions> options)
        {
            return authenticationBuilder.AddScheme<InternalAuthenticationOptions, InternalAuthenticationHandler>(InternalAuthenticationOptions.DefaultScheme, options);
        }
    }
}
