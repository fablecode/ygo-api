using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.UpdateSpellCard
{
    public class UpdateSpellCardCommand : IRequest<CommandResult>
    {
        public long Id { get; set; }
        public int? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> SubCategoryIds { get; set; }
    }
}