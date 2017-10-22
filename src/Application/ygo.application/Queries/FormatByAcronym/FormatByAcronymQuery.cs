using MediatR;
using ygo.application.Dto;

namespace ygo.application.Queries.FormatByAcronym
{
    public class FormatByAcronymQuery : IRequest<FormatDto>
    {
        public string Acronym { get; set; }
    }
}