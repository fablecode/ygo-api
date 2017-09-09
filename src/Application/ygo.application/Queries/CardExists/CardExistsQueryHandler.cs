using System.Threading.Tasks;
using MediatR;
using ygo.application.Repository;

namespace ygo.application.Queries.CardExists
{
    public class CardExistsQueryHandler : IAsyncRequestHandler<CardExistsQuery, bool>
    {
        private readonly ICardRepository _repository;

        public CardExistsQueryHandler(ICardRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CardExistsQuery message)
        {
            return _repository.CardExists(message.Id);
        }
    }
}