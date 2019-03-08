using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateArchetypeSupportCards
{
    public class
        UpdateArchetypeSupportCardsCommandHandler : IRequestHandler<UpdateArchetypeSupportCardsCommand, CommandResult>
    {
        private readonly IValidator<UpdateArchetypeSupportCardsCommand> _validator;
        private readonly IArchetypeSupportCardsService _archetypeSupportCardsService;

        public UpdateArchetypeSupportCardsCommandHandler(IValidator<UpdateArchetypeSupportCardsCommand> validator, IArchetypeSupportCardsService archetypeSupportCardsService)
        {
            _validator = validator;
            _archetypeSupportCardsService = archetypeSupportCardsService;
        }

        public async Task<CommandResult> Handle(UpdateArchetypeSupportCardsCommand request,
            CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                if (request.Cards.Any())
                {
                    var result = await _archetypeSupportCardsService.Update(request.ArchetypeId, request.Cards.Distinct());

                    commandResult.Data = result;
                }

                commandResult.IsSuccessful = true;
            }

            return commandResult;
        }
    }
}