using MediatR;
using ygo.application.Models.Cards.Input;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommand : IRequest<CommandResult>
    {
        public CardInputModel Card { get; set; }
    }
}