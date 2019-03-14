using AutoMapper;
using System;
using System.IO;
using System.Linq;
using ygo.application.Commands.UpdateBanlist;
using ygo.application.Dto;
using ygo.application.Enums;
using ygo.core.Models.Db;

namespace ygo.application.Commands
{
    public static class CommandMapperHelper
    {
        public static CardDto MapToCardDto(this Card card)
        {
            if (card == null)
                return null;

            var response = new CardDto();

            response.Id = card.Id;
            response.CardNumber = card.CardNumber;
            if (card.Name != null)
            {
                response.ImageUrl =
                    $"/api/images/cards/{string.Concat(card.Name.Split(Path.GetInvalidFileNameChars()))}";
                response.Name = card.Name;
            }
            response.Description = card.Description;
            response.CardLevel = card.CardLevel;
            response.CardRank = card.CardRank;
            response.Atk = card.Atk;
            response.Def = card.Def;

            if (card.CardAttribute != null && card.CardAttribute.Any())
                response.Attribute = Mapper.Map<AttributeDto>(card.CardAttribute.First().Attribute);

            if (card.CardLinkArrow != null && card.CardLinkArrow.Any())
                response.Link = card.CardLinkArrow.Count();

            if (card.CardLinkArrow != null && card.CardLinkArrow.Any())
            {
                foreach (var cardLinkArrow in card.CardLinkArrow)
                {
                    response.LinkArrows.Add(new LinkArrowDto { Id = cardLinkArrow.LinkArrowId, Name = cardLinkArrow.LinkArrow.Name });
                }
            }


            if (card.CardSubCategory != null && card.CardSubCategory.Any())
            {
                foreach (var cardSubCategory in card.CardSubCategory)
                {
                    response.SubCategories.Add(new SubCategoryDto { Id = cardSubCategory.SubCategoryId, Name = cardSubCategory.SubCategory.Name, CategoryId = cardSubCategory.SubCategory.CategoryId});
                }
            }

            if (card.CardType != null && card.CardType.Any())
            {
                foreach (var cardType in card.CardType)
                {
                    response.Types.Add(new TypeDto { Id = cardType.TypeId, Name = cardType.Type.Name });
                }
            }


            return response;
        }

        public static void UpdateBanlistWith(this Banlist banlist, UpdateBanlistCommand command)
        {
            banlist.FormatId = command.FormatId;
            banlist.Name = command.Name;
            banlist.ReleaseDate = command.ReleaseDate.GetValueOrDefault();
            banlist.Updated = DateTime.UtcNow;
        }

        public static object MapCardByCardType(YgoCardType cardCardType, Card cardUpdated)
        {
            switch (cardCardType)
            {
                case YgoCardType.Monster:
                    return Mapper.Map<MonsterCardDto>(cardUpdated);
                case YgoCardType.Spell:
                    return Mapper.Map<SpellCardDto>(cardUpdated);
                case YgoCardType.Trap:
                    return Mapper.Map<TrapCardDto>(cardUpdated);
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardCardType), cardCardType, null);
            }
        }

    }
}