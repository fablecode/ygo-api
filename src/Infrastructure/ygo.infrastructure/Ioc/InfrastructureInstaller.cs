using Microsoft.Extensions.DependencyInjection;
using ygo.application.Repository;
using ygo.infrastructure.Repository;

namespace ygo.infrastructure.Ioc
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}