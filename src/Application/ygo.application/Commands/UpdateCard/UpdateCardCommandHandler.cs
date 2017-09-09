using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Enums;

namespace ygo.application.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IAsyncRequestHandler<UpdateCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<UpdateCardCommand> _validator;

        public UpdateCardCommandHandler(IMediator mediator, IValidator<UpdateCardCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public Task<CommandResult> Handle(UpdateCardCommand message)
        {
            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                switch (message.CardType)
                {
                    case YgoCardType.Monster:
                        return _mediator.Send(Mapper.Map<AddMonsterCardCommand>(message));
                    case YgoCardType.Spell:
                        return _mediator.Send(Mapper.Map<AddSpellCardCommand>(message));
                    case YgoCardType.Trap:
                        return _mediator.Send(Mapper.Map<AddTrapCardCommand>(message));
                    default:
                        throw new ArgumentOutOfRangeException(nameof(message.CardType));
                }
            }

            return Task.FromResult(new CommandResult{ Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList()});
        }
    }
}