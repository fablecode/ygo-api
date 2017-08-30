using MediatR;
using ygo.domain.Models;

namespace ygo.application.Queries.CardByName
{
    public class CardByNameQuery : IRequest<Card>
    {
        public string Name { get; set; }
    }
}