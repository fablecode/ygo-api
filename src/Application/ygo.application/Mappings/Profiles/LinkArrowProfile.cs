using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class LinkArrowProfile : Profile
    {
        public LinkArrowProfile()
        {
            CreateMap<LinkArrow, LinkArrowDto>();
        }
    }
}