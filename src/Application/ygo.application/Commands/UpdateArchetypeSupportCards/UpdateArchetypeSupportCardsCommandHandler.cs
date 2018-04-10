using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Commands.UpdateArchetypeCards;
using ygo.domain.Repository;

namespace ygo.application.Commands.UpdateArchetypeSupportCards
{
    public class
        UpdateArchetypeSupportCardsCommandHandler : IRequestHandler<UpdateArchetypeSupportCardsCommand, CommandResult>
    {
        private readonly IValidator<UpdateArchetypeSupportCardsCommand> _validator;
        private readonly IArchetypeSupportCardsRepository _archetypeSupportCardsRepository;

        public UpdateArchetypeSupportCardsCommandHandler(IValidator<UpdateArchetypeSupportCardsCommand> validator, IArchetypeSupportCardsRepository archetypeSupportCardsRepository)
        {
            _validator = validator;
            _archetypeSupportCardsRepository = archetypeSupportCardsRepository;
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
                    var result = await _archetypeSupportCardsRepository.Update(request.ArchetypeId, request.Cards.Distinct());

                    commandResult.Data = result;
                }

                commandResult.IsSuccessful = true;
            }

            return commandResult;
        }
    }
}