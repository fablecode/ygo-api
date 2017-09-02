﻿using System;
using System.Collections.Generic;
using MediatR;
using ygo.domain.Enums;

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommand : IRequest<CommandResult>
    {
        public YgoCardType CardType { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public Uri ImageUrl { get; set; }
        public int AttributeId { get; set; }
        public List<int> SubCategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> LinkArrowIds { get; set; }
    }
}