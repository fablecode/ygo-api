using MediatR;
using ygo.application.Models.Cards.Input;

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommand : IRequest<CommandResult>
    {
        public CardInputModel Card { get; set; }
    }
}