using System;
using System.Collections.Generic;
using System.Linq;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
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
    }
}