using System.IO;
using ygo.application.Commands.AddBanlist;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.UpdateCard;
using ygo.application.Dto;
using ygo.application.Models.Cards.Input;
using ygo.core.Models;
using ygo.core.Models.Db;

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

                cfg.CreateMap<CardSubCategory, CardSubCategoryDto>();


                cfg.CreateMap<Type, TypeDto>();

                cfg.CreateMap<LinkArrow, LinkArrowDto>();

                cfg.CreateMap<Card, MonsterCardDto>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"/api/images/cards/{string.Concat(src.Name.Split(Path.GetInvalidFileNameChars()))}"));

                cfg.CreateMap<Card, SpellCardDto>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"/api/images/cards/{string.Concat(src.Name.Split(Path.GetInvalidFileNameChars()))}"));

                cfg.CreateMap<Card, TrapCardDto>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"/api/images/cards/{string.Concat(src.Name.Split(Path.GetInvalidFileNameChars()))}"));

                cfg.CreateMap<CardInputModel, CardModel>();
                cfg.CreateMap<CardModel, MonsterCardModel>();
                cfg.CreateMap<CardModel, SpellCardModel>();
                cfg.CreateMap<CardModel, TrapCardModel>();

                cfg.CreateMap<AddBanlistCommand, Banlist>();

                cfg.CreateMap<Attribute, AttributeDto>();
                cfg.CreateMap<Banlist, BanlistDto>();
                cfg.CreateMap<BanlistCard, BanlistCardDto>();
                cfg.CreateMap<BanlistCardDto, BanlistCard>();
                cfg.CreateMap<Format, FormatDto>();
                cfg.CreateMap<Limit, LimitDto>();
            });
        }
    }

    public class CardSubCategoryDto
    {
        public long SubCategoryId { get; set; }
        public long CardId { get; set; }
    }
}