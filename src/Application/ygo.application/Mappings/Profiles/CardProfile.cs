using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using ygo.application.Dto;
using ygo.application.Mappings.Resolvers;
using ygo.application.Models.Cards.Input;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.domain.Helpers;

namespace ygo.application.Mappings.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CardInputModel, CardModel>();

            CreateMap<Card, MonsterCardDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<CardImageEndpointResolver>());

            CreateMap<Card, SpellCardDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<CardImageEndpointResolver>());

            CreateMap<Card, TrapCardDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<CardImageEndpointResolver>());

            CreateMap<Card, CardDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<CardImageEndpointResolver>())
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CardLevel, opt => opt.MapFrom(src => src.CardLevel))
                .ForMember(dest => dest.CardRank, opt => opt.MapFrom(src => src.CardRank))
                .ForMember(dest => dest.Atk, opt => opt.MapFrom(src => src.Atk))
                .ForMember(dest => dest.Def, opt => opt.MapFrom(src => src.Def))
                .ForMember(dest => dest.Attribute, opt => opt.MapFrom(delegate(Card card, CardDto cardDto)
                {
                    if (card.CardAttribute != null && card.CardAttribute.Any())
                        return Mapper.Map<AttributeDto>(card.CardAttribute.First().Attribute);

                    return null;
                }))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(delegate(Card card, CardDto cardDto)
                {
                    if (card.CardLinkArrow != null && card.CardLinkArrow.Any())
                        return card.CardLinkArrow.Count();

                    return (int?) null;
                }))
                .ForMember(dest => dest.LinkArrows, opt => opt.MapFrom(delegate(Card card, CardDto cardDto)
                {
                    var linkArrowList = new List<LinkArrowDto>();

                    if (card.CardLinkArrow != null && card.CardLinkArrow.Any())
                    {
                        foreach (var cardLinkArrow in card.CardLinkArrow)
                        {
                            linkArrowList.Add(new LinkArrowDto
                                {Id = cardLinkArrow.LinkArrowId, Name = cardLinkArrow.LinkArrow.Name});
                        }
                    }

                    return linkArrowList;
                }))
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(delegate(Card card, CardDto cardDto)
                {
                    var subCategoryDtos = new List<SubCategoryDto>();

                    if (card.CardSubCategory != null && card.CardSubCategory.Any())
                    {
                        foreach (var cardSubCategory in card.CardSubCategory)
                        {
                            subCategoryDtos.Add
                            (
                                new SubCategoryDto
                                {
                                    Id = cardSubCategory.SubCategoryId,
                                    Name = cardSubCategory.SubCategory.Name,
                                    CategoryId = cardSubCategory.SubCategory.CategoryId
                                });
                        }
                    }

                    return subCategoryDtos;
                }))
                .ForMember(dest => dest.Types, opt => opt.MapFrom(delegate(Card card, CardDto cardDto)
                {
                    var typeDtos = new List<TypeDto>();

                    if (card.CardType != null && card.CardType.Any())
                    {
                        foreach (var cardType in card.CardType)
                        {
                            typeDtos.Add(new TypeDto {Id = cardType.TypeId, Name = cardType.Type.Name});
                        }
                    }

                    return typeDtos;
                }));
        }

    }
}