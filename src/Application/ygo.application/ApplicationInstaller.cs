using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ygo.application.Commands.AddArchetype;
using ygo.application.Commands.AddBanlist;
using ygo.application.Commands.AddCategory;
using ygo.application.Commands.DownloadImage;
using ygo.application.Commands.UpdateArchetype;
using ygo.application.Commands.UpdateArchetypeCards;
using ygo.application.Commands.UpdateArchetypeSupportCards;
using ygo.application.Commands.UpdateBanlist;
using ygo.application.Commands.UpdateBanlistCards;
using ygo.application.Commands.UpdateRulings;
using ygo.application.Commands.UpdateTips;
using ygo.application.Commands.UpdateTrivia;
using ygo.application.Models.Cards.Input;
using ygo.application.Queries.ArchetypeByName;
using ygo.application.Queries.ArchetypeSearch;
using ygo.application.Queries.CardById;
using ygo.application.Queries.CardByName;
using ygo.application.Queries.CategoryById;
using ygo.application.Validations.Cards;
using ygo.core.Services;
using ygo.core.Strategies;
using ygo.domain.Services;
using ygo.domain.Strategies;

namespace ygo.application
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddCqrs();
            services.AddValidators();
            services.AddAutoMapper();
            services.DomainServices();
            services.AddStrategies();

            return services;
        }

        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationInstaller).GetTypeInfo().Assembly);

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CategoryByIdQuery>, CategoryByIdQueryValidator>();
            services.AddTransient<IValidator<AddCategoryCommand>, AddCategoryCommandValidator>();
            services.AddTransient<IValidator<CardInputModel>, CardValidator>();
            services.AddTransient<IValidator<CardByIdQuery>, CardByIdQueryValidator>();
            services.AddTransient<IValidator<CardByNameQuery>, CardByNameQueryValidator>();
            services.AddTransient<IValidator<DownloadImageCommand>, DownloadImageCommandValidator>();
            services.AddTransient<IValidator<AddBanlistCommand>, AddBanlistCommandValidator>();
            services.AddTransient<IValidator<UpdateBanlistCardsCommand>, UpdateBanlistCardsCommandValidator>();
            services.AddTransient<IValidator<UpdateBanlistCommand>, UpdateBanlistCommandValidator>();
            services.AddTransient<IValidator<ArchetypeByNameQuery>, ArchetypeByNameQueryValidator>();
            services.AddTransient<IValidator<AddArchetypeCommand>, AddArchetypeCommandValidator>();
            services.AddTransient<IValidator<UpdateArchetypeCommand>, UpdateArchetypeCommandValidator>();
            services.AddTransient<IValidator<UpdateArchetypeCardsCommand>, UpdateArchetypeCardsCommandValidator>();
            services.AddTransient<IValidator<UpdateArchetypeSupportCardsCommand>, UpdateArchetypeSupportCardsCommandValidator>();
            services.AddTransient<IValidator<ArchetypeSearchQuery>, ArchetypeSearchQueryValidator>();
            services.AddTransient<IValidator<UpdateTipsCommand>, UpdateTipsCommandValidator>();
            services.AddTransient<IValidator<UpdateTriviaCommand>, UpdateTriviaCommandValidator>();
            services.AddTransient<IValidator<UpdateRulingCommand>, UpdateRulingCommandValidator>();



            return services;
        }

        public static IServiceCollection AddStrategies(this IServiceCollection services)
        {

            services.AddTransient<ICardTypeStrategy, MonsterCardTypeStrategy>();
            services.AddTransient<ICardTypeStrategy, SpellCardTypeStrategy>();
            services.AddTransient<ICardTypeStrategy, TrapCardTypeStrategy>();

            return services;
        }
        public static IServiceCollection DomainServices(this IServiceCollection services)
        {

            services.AddTransient<ICardService, CardService>();
            services.AddTransient<ICardRulingService, CardRulingService>();
            services.AddTransient<ICardTipService, CardTipService>();
            services.AddTransient<ICardTriviaService, CardTriviaService>();
            services.AddTransient<IArchetypeService, ArchetypeService>();
            services.AddTransient<IBanlistService, BanlistService>();
            services.AddTransient<IBanlistCardsService, BanlistCardsService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IArchetypeCardsService, ArchetypeCardsService>();
            services.AddTransient<IArchetypeSupportCardsService, ArchetypeSupportCardsService>();
            services.AddTransient<IAttributeService, AttributeService>();
            services.AddTransient<ILimitService, LimitService>();

            return services;
        }

    }
}