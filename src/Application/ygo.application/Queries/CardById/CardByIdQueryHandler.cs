using System.Threading;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using ygo.application.Commands;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.CardById
{
    public class CardByIdQueryHandler : IRequestHandler<CardByIdQuery, CardDto>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<CardByIdQuery> _validator;

        public CardByIdQueryHandler(ICardRepository repository, IValidator<CardByIdQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CardDto> Handle(CardByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var result = await _repository.CardById(request.Id);

                return result.MapToCardDto();
            }

            return null;
        }
    }
}