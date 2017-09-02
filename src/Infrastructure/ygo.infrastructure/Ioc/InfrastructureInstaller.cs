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
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<ITypeRepository, TypeRepository>();
            services.AddTransient<ILinkArrowRepository, LinkArrowRepository>();

            return services;
        }
    }
}