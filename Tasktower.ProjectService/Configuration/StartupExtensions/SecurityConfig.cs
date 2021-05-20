using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.Security;

namespace Tasktower.ProjectService.Configuration.StartupExtensions
{
    public static class SecurityConfig
    {
        public static void ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o => {
                o.Authority = configuration["Authentication:Authority"];
                o.Audience = configuration["Authentication:Audience"];
            });
            
            services.AddAuthorization(options =>
            {
                foreach (var policy in Policies.Get())
                {
                    options.AddPolicy(policy.Name, policyBuilder => 
                        policyBuilder.RequireClaim(System.Security.Claims.ClaimTypes.Role, policy.Roles));
                }
            });
        }

        public static void ConfigureSecurity(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}