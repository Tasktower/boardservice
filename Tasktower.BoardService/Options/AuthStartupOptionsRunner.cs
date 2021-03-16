using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tasktower.BoardService.Security;
using Tasktower.BoardService.Security.Extensions;

namespace Tasktower.BoardService.Options
{
    public static class AuthStartupOptionsRunner
    {
        public static void ConfigureAuthentication(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = HeaderAuthenticationOptions.DefaultScheme;
            options.DefaultChallengeScheme = HeaderAuthenticationOptions.DefaultScheme;
        }

        public static void ConfigureAuthorization(AuthorizationOptions options)
        {
            var policies = typeof(Policies).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(Policy))
                .Select(f => (Policy)f.GetValue(null));
            foreach (var policy in policies)
            {
                options.AddPolicy(policy);
            }
        }
    }
}
