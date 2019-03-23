using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Commands;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.CardById
{
    public class CardByIdQueryHandler : IRequestHandler<CardByIdQuery, CardDto>
    {
        private readonly ICardService _cardService;
        private readonly IValidator<CardByIdQuery> _validator;
        private readonly IMapper _mapper;

        public CardByIdQueryHandler(ICardService cardService, IValidator<CardByIdQuery> validator, IMapper mapper)
        {
            _cardService = cardService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CardDto> Handle(CardByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var result = await _cardService.CardById(request.Id);

                return CommandMapperHelper.MapToCardDto(_mapper, result);
            }

            return null;
        }
    }
}