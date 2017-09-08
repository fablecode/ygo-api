using System;
using System.Collections.Generic;
using System.Linq;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Commands.UpdateTrapCard;
using ygo.domain.Models;

namespace ygo.application.Commands
{
    public static class CommandMapperHelper
    {
        public static Card MapToCard(this AddMonsterCardCommand command)
        {
            var newMonsterCard = new Card
            {
                CardNumber = command.CardNumber.HasValue ? command.CardNumber.ToString() : null,
                Name = command.Name,
                Description = command.Description,
                CardLevel = command.CardLevel,
                CardRank = command.CardRank,
                Atk = command.Atk,
                Def = command.Def,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            newMonsterCard.CardAttribute.Add(new CardAttribute { AttributeId = command.AttributeId });

            if (command.SubCategoryIds.Any())
            {
                foreach (var sbIds in command.SubCategoryIds)
                    newMonsterCard.CardSubCategory.Add(new CardSubCategory { SubCategoryId = sbIds });
            }

            if (command.TypeIds.Any())
            {
                foreach (var typeId in command.TypeIds)
                    newMonsterCard.CardType.Add(new CardType { TypeId = typeId});
            }

            if (command.LinkArrowIds.Any())
            {
                foreach (var linkArrowId in command.LinkArrowIds)
                    newMonsterCard.CardLinkArrow.Add(new CardLinkArrow{ LinkArrowId = linkArrowId});
            }

            return newMonsterCard;
        }

        public static Card MapToCard(this AddSpellCardCommand command)
        {
            var newSpellCard = MapToSpellOrTrapCard(command.CardNumber, command.Name, command.Description, command.SubCategoryIds);

            return newSpellCard;
        }

        public static Card MapToCard(this AddTrapCardCommand command)
        {
            var newTrapCard = MapToSpellOrTrapCard(command.CardNumber, command.Name, command.Description, command.SubCategoryIds);

            return newTrapCard;
        }

        public static Card MapToCard(this UpdateSpellCardCommand command)
        {
            var updateSpellCard = MapToSpellOrTrapCard(command.CardNumber, command.Name, command.Description, command.SubCategoryIds);

            return updateSpellCard;
        }

        public static Card MapToCard(this UpdateTrapCardCommand command)
        {
            var updateTrapCard = MapToSpellOrTrapCard(command.CardNumber, command.Name, command.Description, command.SubCategoryIds);

            return updateTrapCard;
        }

        public static void UpdateMonsterCardWith(this Card card, UpdateMonsterCardCommand command)
        {
            card.CardNumber = command.CardNumber.HasValue ? command.CardNumber.ToString() : null;
            card.Name = command.Name;
            card.Description = command.Description;
            card.CardLevel = command.CardLevel;
            card.CardRank = command.CardRank;
            card.Atk = command.Atk;
            card.Def = command.Def;
            card.Updated = DateTime.UtcNow;

            // Clear monster related data.
            card.CardAttribute.Clear();
            card.CardSubCategory.Clear();
            card.CardType.Clear();

            card.CardAttribute.Add(new CardAttribute { AttributeId = command.AttributeId });

            if (command.SubCategoryIds.Any())
            {
                foreach (var sbIds in command.SubCategoryIds)
                    card.CardSubCategory.Add(new CardSubCategory { SubCategoryId = sbIds });
            }

            if (command.TypeIds.Any())
            {
                foreach (var typeId in command.TypeIds)
                    card.CardType.Add(new CardType { TypeId = typeId });
            }

            if (command.LinkArrowIds.Any())
            {
                foreach (var linkArrowId in command.LinkArrowIds)
                    card.CardLinkArrow.Add(new CardLinkArrow { LinkArrowId = linkArrowId });
            }
        }

        public static Card MapToSpellOrTrapCard(int? cardNumber, string name, string description, IList<int> subCategoryIds)
        {
            var newCard = new Card
            {
                Name = name,
                CardNumber = cardNumber.HasValue ? cardNumber.ToString() : null,
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

        public static void MapToSpellOrTrapCard(this Card card, int? cardNumber, string name, string description, IList<int> subCategoryIds)
        {
            card.Name = name;
            card.CardNumber = cardNumber.HasValue ? cardNumber.ToString() : null;
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