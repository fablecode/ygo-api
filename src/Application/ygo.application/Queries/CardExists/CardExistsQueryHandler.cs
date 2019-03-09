using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Services;

namespace ygo.application.Queries.CardExists
{
    public class CardExistsQueryHandler : IRequestHandler<CardExistsQuery, bool>
    {
        private readonly ICardService _cardService;

        public CardExistsQueryHandler(ICardService cardService)
        {
            _cardService = cardService;
        }

        public Task<bool> Handle(CardExistsQuery request, CancellationToken cancellationToken)
        {
            return _cardService.CardExists(request.Id);
        }
    }
}