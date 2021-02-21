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
using Tasktower.Webtools.Security.Auth;
using Tasktower.Webtools.Security.Auth.Extensions;

namespace Tasktower.BoardService.Options
{
    public static class AuthStartupOptionsRunner
    {
        public static void ConfigureAuthentication(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        public static void ConfigureJWTBearer(IConfiguration configuration, JwtBearerOptions o)
        {
            o.Authority = configuration["Auth:Authority"];
            o.Audience = configuration["Auth:Audience"];
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
