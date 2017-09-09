using System.Linq;
using ygo.application.Dto;
using ygo.application.Ioc;
using ygo.domain.Models;

namespace ygo.application
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();

                cfg.CreateMap<SubCategory, SubCategoryDto>()
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

                cfg.CreateMap<Type, TypeDto>();

                cfg.CreateMap<LinkArrow, LinkArrowDto>();

                cfg.CreateMap<Card, MonsterCardDto>()
                    .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.CardSubCategory));

                cfg.CreateMap<Card, SpellCardDto>()
                    .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.CardSubCategory.SingleOrDefault()));

                cfg.CreateMap<Card, TrapCardDto>()
                    .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.CardSubCategory.SingleOrDefault()));

                cfg.CreateMap<Card, CardDto>();
            });
        }
    }
}