using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.BanlistById
{
    public class BanlistByIdQuery : IRequest<BanlistDto>
    {
        public long Id { get; set; }
    }
}