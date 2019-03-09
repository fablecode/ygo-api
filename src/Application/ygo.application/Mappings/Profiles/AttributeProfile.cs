using AutoMapper;
using ygo.application.Dto;
using ygo.application.Models.Cards.Input;
using ygo.core.Models;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile()
        {
            CreateMap<Attribute, AttributeDto>();
        }
    }
}