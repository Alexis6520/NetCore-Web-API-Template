using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services;

namespace Infrastructure.EFCore.SqlServer
{
    public class SqlServerDbContext(IConfiguration configuration) : ApplicationDbContext
    {
        private readonly IConfiguration _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = _configuration.GetConnectionString("Main");
            optionsBuilder.UseSqlServer(connString);
        }
    }
}
