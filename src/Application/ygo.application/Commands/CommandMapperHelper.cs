using System;
using System.Linq;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.domain.Models;

namespace ygo.application.Commands
{
    public static class CommandMapperHelper
    {
        public static Card MapToCard(this AddMonsterCardCommand command)
        {
            var newMonsterCard = new Card
            {
                CardNumber = command.CardNumber.ToString(),
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
            var newSpellCard = new Card
            {
                Name = command.Name,
                CardNumber = command.CardNumber.ToString(),
                Description = command.Description,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };


            if (command.SubCategoryIds.Any())
            {
                foreach (var sbIds in command.SubCategoryIds)
                    newSpellCard.CardSubCategory.Add(new CardSubCategory { SubCategoryId = sbIds });
            }

            return newSpellCard;
        }
    }
}