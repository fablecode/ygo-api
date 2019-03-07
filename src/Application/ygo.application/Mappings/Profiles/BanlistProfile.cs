using AutoMapper;
using ygo.application.Commands.AddBanlist;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class BanlistProfile : Profile
    {
        public BanlistProfile()
        {
            CreateMap<AddBanlistCommand, Banlist>();
            CreateMap<Banlist, BanlistDto>();
            CreateMap<BanlistCard, BanlistCardDto>();
            CreateMap<BanlistCardDto, BanlistCard>();
        }
    }
}