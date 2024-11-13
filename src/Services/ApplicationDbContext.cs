using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public abstract class ApplicationDbContext : DbContext
    {
        public DbSet<Donut> Donuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var contextType = GetType();
            var configNamespace = $"{contextType.Namespace}.Configuration";

            modelBuilder.ApplyConfigurationsFromAssembly(
                contextType.Assembly,
                type => type.Namespace == configNamespace);
        }
    }
}
