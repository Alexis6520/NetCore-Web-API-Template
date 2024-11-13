using Microsoft.EntityFrameworkCore;
using Services;

namespace WebAPI.Extensions
{
    public static class WebApplicationExtension
    {
        public static void InitializeDatabase(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();

            using var dbContext = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
