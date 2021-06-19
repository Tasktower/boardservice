using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasktower.ProjectService.DataAccess.Context;

namespace Tasktower.ProjectService.Tests.TestHelpers.DependencyInjection
{
    public static class TestDataBaseConnectionConfig
    {
        public const string DbName = "testDb";
            
        public static void ConfigureInMemoryDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<BoardDBContext>(options =>
            {
                options.UseInMemoryDatabase(DbName);
            });
        }
    }
}