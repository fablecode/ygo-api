using System.IO;
using ygo.application.Commands.AddBanlist;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Commands.UpdateCard;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTrapCard;
using ygo.application.Dto;
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

                cfg.CreateMap<AddCardCommand, AddMonsterCardCommand>();
                cfg.CreateMap<AddCardCommand, AddSpellCardCommand>();
                cfg.CreateMap<AddCardCommand, AddTrapCardCommand>();
                cfg.CreateMap<AddBanlistCommand, Banlist>();
                cfg.CreateMap<UpdateCardCommand, UpdateMonsterCardCommand>();
                cfg.CreateMap<UpdateCardCommand, UpdateSpellCardCommand>();
                cfg.CreateMap<UpdateCardCommand, UpdateTrapCardCommand>();

                cfg.CreateMap<Attribute, AttributeDto>();
                cfg.CreateMap<Banlist, BanlistDto>();
                cfg.CreateMap<BanlistCard, BanlistCardDto>();
                cfg.CreateMap<Format, FormatDto>();
            });
        }
    }

    public class CardSubCategoryDto
    {
        public long SubCategoryId { get; set; }
        public long CardId { get; set; }
    }
}