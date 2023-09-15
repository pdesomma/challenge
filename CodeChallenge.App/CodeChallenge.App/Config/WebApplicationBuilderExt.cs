using CodeChallenge.Repos.FileRepo;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.App.Config
{
    public static class WebApplicationBuilderExt
    {
        private static readonly string DB_NAME = "EmployeeDB";
        public static void UseEmployeeDB(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseInMemoryDatabase(DB_NAME);
            });
        }
    }
}
