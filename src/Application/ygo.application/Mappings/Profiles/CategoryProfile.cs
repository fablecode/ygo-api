using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}