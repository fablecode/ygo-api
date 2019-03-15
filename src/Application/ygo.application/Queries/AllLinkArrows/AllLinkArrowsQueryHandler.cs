using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.core.Services;

namespace ygo.application.Queries.AllLinkArrows
{
    public class AllLinkArrowsQueryHandler : IRequestHandler<AllLinkArrowsQuery, IEnumerable<LinkArrowDto>>
    {
        private readonly ILinkArrowService _linkArrowService;
        private readonly IMapper _mapper;

        public AllLinkArrowsQueryHandler(ILinkArrowService linkArrowService, IMapper mapper)
        {
            _linkArrowService = linkArrowService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LinkArrowDto>> Handle(AllLinkArrowsQuery request, CancellationToken cancellationToken)
        {
            var linkArrows = await _linkArrowService.AllLinkArrows();

            return _mapper.Map<IEnumerable<LinkArrowDto>>(linkArrows);
        }
    }
}