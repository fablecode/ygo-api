using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ygo.application.Commands.AddArchetype;
using ygo.application.Commands.AddBanlist;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.AddCategory;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Commands.DownloadImage;
using ygo.application.Commands.UpdateArchetype;
using ygo.application.Commands.UpdateArchetypeCards;
using ygo.application.Commands.UpdateArchetypeSupportCards;
using ygo.application.Commands.UpdateBanlist;
using ygo.application.Commands.UpdateBanlistCards;
using ygo.application.Commands.UpdateCard;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateRulings;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTips;
using ygo.application.Commands.UpdateTrapCard;
using ygo.application.Commands.UpdateTrivia;
using ygo.application.Queries.ArchetypeByName;
using ygo.application.Queries.ArchetypeSearch;
using ygo.application.Queries.CardById;
using ygo.application.Queries.CardByName;
using ygo.application.Queries.CategoryById;
using ygo.application.Queries.FormatByAcronym;

namespace ygo.application.Ioc
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddCqrs();
            services.AddValidators();
            services.AddAutoMapper();

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
            services.AddTransient<IValidator<AddCardCommand>, AddCardCommandValidator>();
            services.AddTransient<IValidator<AddMonsterCardCommand>, AddMonsterCardCommandValidator>();
            services.AddTransient<IValidator<AddSpellCardCommand>, AddSpellCardCommandValidator>();
            services.AddTransient<IValidator<AddTrapCardCommand>, AddTrapCardCommandValidator>();
            services.AddTransient<IValidator<UpdateCardCommand>, UpdateCardCommandValidator>();
            services.AddTransient<IValidator<UpdateMonsterCardCommand>, UpdateMonsterCardCommandValidator>();
            services.AddTransient<IValidator<UpdateSpellCardCommand>, UpdateSpellCardCommandValidator>();
            services.AddTransient<IValidator<UpdateTrapCardCommand>, UpdateTrapCardCommandValidator>();
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

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfig.Configure();

            return services;
        }
    }
}