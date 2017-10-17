using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Commands;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.CardById
{
    public class CardByIdQueryHandler : IAsyncRequestHandler<CardByIdQuery, CardDto>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<CardByIdQuery> _validator;

        public CardByIdQueryHandler(ICardRepository repository, IValidator<CardByIdQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CardDto> Handle(CardByIdQuery message)
        {
            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                var result = await  _repository.CardById(message.Id);

                return result.MapToCardDto();
            }

            return null;
        }
    }
}