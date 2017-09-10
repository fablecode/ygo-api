﻿using System.Linq;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Commands.UpdateCard;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTrapCard;
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

                cfg.CreateMap<AddCardCommand, AddMonsterCardCommand>();
                cfg.CreateMap<AddCardCommand, AddSpellCardCommand>();
                cfg.CreateMap<AddCardCommand, AddTrapCardCommand>();
                cfg.CreateMap<UpdateCardCommand, UpdateMonsterCardCommand>();
                cfg.CreateMap<UpdateCardCommand, UpdateSpellCardCommand>();
                cfg.CreateMap<UpdateCardCommand, UpdateTrapCardCommand>();
            });
        }
    }
}