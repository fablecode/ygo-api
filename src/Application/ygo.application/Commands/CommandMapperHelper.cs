using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ygo.application.Commands.UpdateBanlist;
using ygo.application.Dto;
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

        public static Card MapToSpellOrTrapCard(long? cardNumber, string name, string description, IList<int> subCategoryIds)
        {
            return MapToSpellOrTrapCard(0, cardNumber, name, description, subCategoryIds);
        }

        public static Card MapToSpellOrTrapCard(long id, long? cardNumber, string name, string description, IList<int> subCategoryIds)
        {
            var newCard = new Card
            {
                Id = id,
                Name = name,
                CardNumber = cardNumber,
                Description = description,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            if (subCategoryIds.Any())
            {
                foreach (var scId in subCategoryIds)
                    newCard.CardSubCategory.Add(new CardSubCategory { SubCategoryId = scId });
            }

            return newCard;
        }

        public static void MapToSpellOrTrapCard(this Card card, long? cardNumber, string name, string description, IList<int> subCategoryIds)
        {
            card.Name = name;
            card.CardNumber = cardNumber;
            card.Description = description;
            card.Updated = DateTime.UtcNow;

            subCategoryIds.Clear();

            if (subCategoryIds.Any())
            {
                foreach (var scId in subCategoryIds)
                    card.CardSubCategory.Add(new CardSubCategory { SubCategoryId = scId });
            }
        }
    }
}