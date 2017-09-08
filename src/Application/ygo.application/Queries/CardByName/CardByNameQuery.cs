using MediatR;
using ygo.application.Dto;
using ygo.domain.Models;

namespace ygo.application.Queries.CardByName
{
    public class CardByNameQuery : IRequest<CardDto>
    {
        public string Name { get; set; }
    }
}