using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategory, SubCategoryDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
        }
    }
}