using System;
using System.Collections.Generic;
using MediatR;
using ygo.application.Enums;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommand : IRequest<CommandResult>
    {
        public YgoCardType? CardType { get; set; }

        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }

        public Uri ImageUrl { get; set; }

        public List<string> SubCategories { get; set; }
        public List<string> Types { get; set; }
        public List<string> LinkArrows { get; set; }
    }
}