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

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommandHandler : IAsyncRequestHandler<AddCardCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddCardCommand> _validator;

        public AddCardCommandHandler(IMediator mediator, IValidator<AddCardCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public Task<CommandResult> Handle(AddCardCommand message)
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