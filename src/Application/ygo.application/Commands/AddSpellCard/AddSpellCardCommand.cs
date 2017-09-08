﻿using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.AddSpellCard
{
    public class AddSpellCardCommand : IRequest<CommandResult>
    {
        public int? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> SubCategoryIds { get; set; }
    }
}