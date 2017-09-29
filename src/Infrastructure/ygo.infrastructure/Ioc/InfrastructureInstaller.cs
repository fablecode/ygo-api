using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ygo.application.Repository;
using ygo.application.Service;
using ygo.infrastructure.Database;
using ygo.infrastructure.Repository;
using ygo.infrastructure.Service;

namespace ygo.infrastructure.Ioc
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IFileSystemService, FileSystemService>();

            services.AddYgoDatabase(connectionString);
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddYgoDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<YgoDbContext>(c => c.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<ITypeRepository, TypeRepository>();
            services.AddTransient<ILinkArrowRepository, LinkArrowRepository>();
            services.AddTransient<IAttributeRepository, AttributeRepository>();

            return services;
        }
    }
}