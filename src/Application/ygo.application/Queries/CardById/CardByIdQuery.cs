using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.CardById
{
    public class CardByIdQuery : IRequest<CardDto>
    {
        public long Id { get; set; }
    }
}