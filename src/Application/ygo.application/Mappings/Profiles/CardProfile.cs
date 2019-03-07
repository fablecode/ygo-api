using AutoMapper;
using ygo.application.Models.Cards.Input;
using ygo.core.Models;

namespace ygo.application.Mappings.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CardInputModel, CardModel>();
        }
    }
}