using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.AddCategory;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Queries.CategoryById;
using ygo.domain.Models;

namespace ygo.application.Ioc
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationInstaller).GetTypeInfo().Assembly);

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();

                cfg.CreateMap<SubCategory, SubCategoryDto>()
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

                cfg.CreateMap<Card, MonsterCardDto>()
                    .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.CardSubCategory));

                cfg.CreateMap<Card, SpellCardDto>()
                    .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.CardSubCategory.SingleOrDefault()));

                cfg.CreateMap<Card, TrapCardDto>()
                    .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.CardSubCategory.SingleOrDefault()));
            });

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

            return services;
        }

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

        public List<SubCategoryDto> SubCategories { get; set; }
    }

    public class SpellCardDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Description { get; set; }
        public SubCategoryDto SubCategory { get; set; }
    }

    public class SubCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CategoryDto Category { get; set; }
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
        public SubCategoryDto SubCategory { get; set; }
    }

}