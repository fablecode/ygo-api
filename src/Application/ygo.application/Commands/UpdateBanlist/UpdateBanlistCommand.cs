using System;
using MediatR;

namespace ygo.application.Commands.UpdateBanlist
{
    public class UpdateBanlistCommand : IRequest<CommandResult>
    {
        public long Id { get; set; }
        public long FormatId { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}