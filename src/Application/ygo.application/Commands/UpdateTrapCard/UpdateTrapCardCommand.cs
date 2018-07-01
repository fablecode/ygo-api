﻿using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.UpdateTrapCard
{
    public class UpdateTrapCardCommand : IRequest<CommandResult>
    {
        public long Id { get; set; }
        public long? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> SubCategoryIds { get; set; }
    }
}