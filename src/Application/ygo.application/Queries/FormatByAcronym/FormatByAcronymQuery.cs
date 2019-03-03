using MediatR;
using ygo.application.Dto;
using ygo.application.Enums;

namespace ygo.application.Queries.FormatByAcronym
{
    public class FormatByAcronymQuery : IRequest<FormatDto>
    {
        public BanlistFormat Acronym { get; set; }
    }
}