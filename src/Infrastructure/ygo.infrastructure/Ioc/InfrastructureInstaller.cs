using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ygo.application.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Repository;

namespace ygo.infrastructure.Ioc
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddYgoDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<YgoDbContext>(c => c.UseSqlServer(connectionString));
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}