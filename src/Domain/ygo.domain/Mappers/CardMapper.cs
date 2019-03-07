using System;
using System.Collections.Generic;
using System.Linq;
using ygo.core.Models;
using ygo.core.Models.Db;

namespace ygo.domain.Mappers
{
    public static class CardMapper
    {
        public static Card MapToMonsterCard(CardModel cardModel)
        {
            var newMonsterCard = new Card
            {
                CardNumber = cardModel.CardNumber,
                Name = cardModel.Name,
                Description = cardModel.Description,
                CardLevel = cardModel.CardLevel,
                CardRank = cardModel.CardRank,
                Atk = cardModel.Atk,
                Def = cardModel.Def,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            newMonsterCard.CardAttribute.Add(new CardAttribute { AttributeId = cardModel.AttributeId.GetValueOrDefault() });

            if (cardModel.SubCategoryIds.Any())
            {
                foreach (var sbIds in cardModel.SubCategoryIds)
                    newMonsterCard.CardSubCategory.Add(new CardSubCategory { SubCategoryId = sbIds });
            }

            if (cardModel.TypeIds.Any())
            {
                foreach (var typeId in cardModel.TypeIds)
                    newMonsterCard.CardType.Add(new CardType { TypeId = typeId });
            }

            if (cardModel.LinkArrowIds.Any())
            {
                foreach (var linkArrowId in cardModel.LinkArrowIds)
                    newMonsterCard.CardLinkArrow.Add(new CardLinkArrow { LinkArrowId = linkArrowId });
            }

            return newMonsterCard;
        }

        public static Card MapToSpellOrTrapCard(CardModel command)
        {
            var card = MapToSpellOrTrapCard(command.CardNumber, command.Name, command.Description, command.SubCategoryIds);

            return card;
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

        public static void UpdateMonsterCardWith(Card card, CardModel cardModel)
        {
            card.CardNumber = cardModel.CardNumber;
            card.Name = cardModel.Name;
            card.Description = cardModel.Description;
            card.CardLevel = cardModel.CardLevel;
            card.CardRank = cardModel.CardRank;
            card.Atk = cardModel.Atk;
            card.Def = cardModel.Def;
            card.Updated = DateTime.UtcNow;

            // Clear monster related data.
            card.CardAttribute.Clear();
            card.CardSubCategory.Clear();
            card.CardType.Clear();

            card.CardAttribute.Add(new CardAttribute { AttributeId = cardModel.AttributeId.GetValueOrDefault(), CardId = card.Id });

            if (cardModel.SubCategoryIds.Any())
            {
                foreach (var sbIds in cardModel.SubCategoryIds)
                    card.CardSubCategory.Add(new CardSubCategory { SubCategoryId = sbIds, CardId = card.Id });
            }

            if (cardModel.TypeIds.Any())
            {
                foreach (var typeId in cardModel.TypeIds)
                    card.CardType.Add(new CardType { TypeId = typeId, CardId = card.Id });
            }

            if (cardModel.LinkArrowIds.Any())
            {
                foreach (var linkArrowId in cardModel.LinkArrowIds)
                    card.CardLinkArrow.Add(new CardLinkArrow { LinkArrowId = linkArrowId, CardId = card.Id });
            }
        }

        public static void UpdateSpellCardWith(Card card, CardModel cardModel)
        {
            card.CardNumber = cardModel.CardNumber;
            card.Name = cardModel.Name;
            card.Description = cardModel.Description;
            card.Updated = DateTime.UtcNow;

            // Clear monster related data.
            card.CardSubCategory.Clear();

            if (cardModel.SubCategoryIds.Any())
            {
                foreach (var sbIds in cardModel.SubCategoryIds)
                    card.CardSubCategory.Add(new CardSubCategory { SubCategoryId = sbIds, CardId = card.Id });
            }
        }

        public static void UpdateTrapCardWith(Card card, CardModel cardModel)
        {
            card.CardNumber = cardModel.CardNumber;
            card.Name = cardModel.Name;
            card.Description = cardModel.Description;
            card.Updated = DateTime.UtcNow;

            // Clear monster related data.
            card.CardSubCategory.Clear();

            if (cardModel.SubCategoryIds.Any())
            {
                foreach (var sbIds in cardModel.SubCategoryIds)
                    card.CardSubCategory.Add(new CardSubCategory { SubCategoryId = sbIds, CardId = card.Id });
            }
        }

    }
}