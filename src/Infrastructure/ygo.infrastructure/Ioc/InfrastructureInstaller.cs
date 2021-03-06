﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ygo.domain.Repository;
using ygo.domain.SystemIO;
using ygo.infrastructure.Database;
using ygo.infrastructure.Repository;
using ygo.infrastructure.SystemIO;

namespace ygo.infrastructure.Ioc
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IFileSystem, FileSystem>();

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
            services.AddTransient<IBanlistRepository, BanlistRepository>();
            services.AddTransient<IBanlistCardsRepository, BanlistCardsRepository>();
            services.AddTransient<ILimitRepository, LimitRepository>();
            services.AddTransient<IFormatRepository, FormatRepository>();
            services.AddTransient<IArchetypeRepository, ArchetypeRepository>();
            services.AddTransient<IArchetypeCardsRepository, ArchetypeCardsRepository>();
            services.AddTransient<IArchetypeSupportCardsRepository, ArchetypeSupportCardsRepository>();
            services.AddTransient<ICardTipRepository, CardTipRepository>();
            services.AddTransient<ICardRulingRepository, CardRulingRepository>();
            services.AddTransient<ICardTriviaRepository, CardTriviaRepository>();

            return services;
        }
    }
}