using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class ArchetypeProfile : Profile
    {
        public ArchetypeProfile()
        {
            CreateMap<Archetype, ArchetypeDto>();
            CreateMap<ArchetypeCard, ArchetypeCardDto>();
        }
    }
}