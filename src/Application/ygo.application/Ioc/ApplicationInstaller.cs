using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.AddCategory;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Commands.DownloadImage;
using ygo.application.Commands.UpdateCard;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTrapCard;
using ygo.application.Queries.CardById;
using ygo.application.Queries.CardByName;
using ygo.application.Queries.CategoryById;

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

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfig.Configure();

            return services;
        }
    }

    public class LinkArrowDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class TypeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class MonsterCardDto
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public string ImageUrl { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; }
    }

    public class SpellCardDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public SubCategoryDto SubCategory { get; set; }
    }

    public class SubCategoryDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class TrapCardDto
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public SubCategoryDto SubCategory { get; set; }
    }

}