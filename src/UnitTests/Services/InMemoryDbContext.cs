using Microsoft.EntityFrameworkCore;
using Services;

namespace UnitTests.Services
{
    public class InMemoryDbContext(string databaseName) : ApplicationDbContext
    {
        private readonly string _databaseName = databaseName;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_databaseName);
        }
    }
}
