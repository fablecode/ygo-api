using System.IO;
using AutoMapper;
using ygo.application.Dto;
using ygo.application.Models.Cards.Input;
using ygo.core.Models;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CardInputModel, CardModel>();

            CreateMap<Card, MonsterCardDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"/api/images/cards/{string.Concat(src.Name.Split(Path.GetInvalidFileNameChars()))}"));

            CreateMap<Card, SpellCardDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"/api/images/cards/{string.Concat(src.Name.Split(Path.GetInvalidFileNameChars()))}"));

            CreateMap<Card, TrapCardDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"/api/images/cards/{string.Concat(src.Name.Split(Path.GetInvalidFileNameChars()))}"));

            CreateMap<CardInputModel, CardModel>();

        }
    }
}