using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.CardByName
{
    public class CardByNameQuery : IRequest<CardDto>
    {
        public string Name { get; set; }
    }
}