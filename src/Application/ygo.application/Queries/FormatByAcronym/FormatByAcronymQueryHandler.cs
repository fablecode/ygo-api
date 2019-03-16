using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.FormatByAcronym
{
    public class FormatByAcronymQueryHandler : IRequestHandler<FormatByAcronymQuery, FormatDto>
    {
        private readonly IFormatService _formatService;
        private readonly IMapper _mapper;

        public FormatByAcronymQueryHandler(IFormatService formatService, IMapper mapper)
        {
            _formatService = formatService;
            _mapper = mapper;
        }

        public async Task<FormatDto> Handle(FormatByAcronymQuery request, CancellationToken cancellationToken)
        {
            var result = await _formatService.FormatByAcronym(request.Acronym.ToString());

            return result != null ? _mapper.Map<FormatDto>(result) : null;
        }
    }
}