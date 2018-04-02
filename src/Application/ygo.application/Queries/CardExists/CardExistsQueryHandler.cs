using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ygo.domain.Repository;

namespace ygo.application.Queries.CardExists
{
    public class CardExistsQueryHandler : IRequestHandler<CardExistsQuery, bool>
    {
        private readonly ICardRepository _repository;

        public CardExistsQueryHandler(ICardRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CardExistsQuery request, CancellationToken cancellationToken)
        {
            return _repository.CardExists(request.Id);
        }
    }
}