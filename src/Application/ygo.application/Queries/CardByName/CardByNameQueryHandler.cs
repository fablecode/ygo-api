using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Queries.CardByName
{
    public class CardByNameQueryHandler : IAsyncRequestHandler<CardByNameQuery, Card>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<CardByNameQuery> _validator;

        public CardByNameQueryHandler(ICardRepository repository, IValidator<CardByNameQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public Task<Card> Handle(CardByNameQuery message)
        {
            var validationResults = _validator.Validate(message);

            return validationResults.IsValid ? _repository.CardByName(message.Name) : null;
        }
    }
}