using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class LimitProfile : Profile
    {
        public LimitProfile()
        {
            CreateMap<Limit, LimitDto>();
        }
    }
}