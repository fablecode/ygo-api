using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class FormatProfile : Profile
    {
        public FormatProfile()
        {
            CreateMap<Format, FormatDto>();
        }
    }
}