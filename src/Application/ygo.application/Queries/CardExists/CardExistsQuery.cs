using MediatR;

namespace ygo.application.Queries.CardExists
{
    public class CardExistsQuery : IRequest<bool>
    {
        public long Id { get; set; }
    }
}