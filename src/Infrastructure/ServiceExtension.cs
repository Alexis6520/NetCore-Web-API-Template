using Infrastructure.EFCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Infrastructure
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext, SqlServerDbContext>();
            return services;
        }
    }
}
