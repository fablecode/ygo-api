using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.CardByName
{
    public class CardByNameQueryHandler : IRequestHandler<CardByNameQuery, CardDto>
    {
        private readonly ICardService _cardService;
        private readonly IValidator<CardByNameQuery> _validator;

        public CardByNameQueryHandler(ICardService cardService, IValidator<CardByNameQuery> validator)
        {
            _cardService = cardService;
            _validator = validator;
        }

        public async Task<CardDto> Handle(CardByNameQuery request, CancellationToken cancellationToken)
        {
            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var result = await _cardService.CardByName(request.Name);

                return result.MapToCardDto();
            }

            return null;
        }
    }
}