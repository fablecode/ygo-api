using System;
using System.Collections.Generic;
using MediatR;
using ygo.core.Enums;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommand : IRequest<CommandResult>
    {
        public long Id { get; set; }
        public YgoCardType? CardType { get; set; }
        public long? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public Uri ImageUrl { get; set; }
        public int? AttributeId { get; set; }
        public List<int> SubCategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> LinkArrowIds { get; set; }
    }
}