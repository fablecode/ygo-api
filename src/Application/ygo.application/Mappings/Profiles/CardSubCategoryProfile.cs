using AutoMapper;
using ygo.application.Dto;
using ygo.core.Models.Db;

namespace ygo.application.Mappings.Profiles
{
    public class CardSubCategoryProfile : Profile
    {
        public CardSubCategoryProfile()
        {
            CreateMap<CardSubCategory, CardSubCategoryDto>();
        }
    }
}