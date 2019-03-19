using System.Collections.Generic;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateArchetypeCards
{
    public class UpdateArchetypeCardsCommandHandler : IRequestHandler<UpdateArchetypeCardsCommand, CommandResult>
    {
        private readonly IValidator<UpdateArchetypeCardsCommand> _validator;
        private readonly IArchetypeCardsService _archetypeCardsService;
        private readonly IMapper _mapper;

        public UpdateArchetypeCardsCommandHandler(IValidator<UpdateArchetypeCardsCommand> validator, IArchetypeCardsService archetypeCardsService, IMapper mapper)
        {
            _validator = validator;
            _archetypeCardsService = archetypeCardsService;
            _mapper = mapper;
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

                    commandResult.Data = _mapper.Map<IEnumerable<ArchetypeCardDto>>(result);
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