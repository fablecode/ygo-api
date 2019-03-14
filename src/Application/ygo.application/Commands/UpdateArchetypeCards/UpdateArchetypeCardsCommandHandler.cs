using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateArchetypeCards
{
    public class UpdateArchetypeCardsCommandHandler : IRequestHandler<UpdateArchetypeCardsCommand, CommandResult>
    {
        private readonly IValidator<UpdateArchetypeCardsCommand> _validator;
        private readonly IArchetypeCardsService _archetypeCardsService;

        public UpdateArchetypeCardsCommandHandler(IValidator<UpdateArchetypeCardsCommand> validator, IArchetypeCardsService archetypeCardsService)
        {
            _validator = validator;
            _archetypeCardsService = archetypeCardsService;
        }
        public async Task<CommandResult> Handle(UpdateArchetypeCardsCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                if (request.Cards.Any())
                {
                    var result = await _archetypeCardsService.Update(request.ArchetypeId, request.Cards.Distinct());

                    commandResult.Data = result;
                }

                commandResult.IsSuccessful = true;
            }
            else
            {
                commandResult.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return commandResult;
        }
    }
}