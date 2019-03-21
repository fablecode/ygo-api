using System.Collections;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Commands.UpdateArchetypeSupportCards
{
    public class UpdateArchetypeSupportCardsCommandHandler : IRequestHandler<UpdateArchetypeSupportCardsCommand, CommandResult>
    {
        private readonly IValidator<UpdateArchetypeSupportCardsCommand> _validator;
        private readonly IArchetypeSupportCardsService _archetypeSupportCardsService;
        private readonly IMapper _mapper;

        public UpdateArchetypeSupportCardsCommandHandler
        (
            IValidator<UpdateArchetypeSupportCardsCommand> validator, 
            IArchetypeSupportCardsService archetypeSupportCardsService,
            IMapper mapper
        )
        {
            _validator = validator;
            _archetypeSupportCardsService = archetypeSupportCardsService;
            _mapper = mapper;
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

                    commandResult.Data = _mapper.Map<IEnumerable<ArchetypeDto>>(result);
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