using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Commands;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.CardByName
{
    public class CardByNameQueryHandler : IAsyncRequestHandler<CardByNameQuery, CardDto>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<CardByNameQuery> _validator;

        public CardByNameQueryHandler(ICardRepository repository, IValidator<CardByNameQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CardDto> Handle(CardByNameQuery message)
        {
            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                var result = await _repository.CardByName(message.Name);

                return result.MapToCardDto();
            }

            return null;
        }
    }
}