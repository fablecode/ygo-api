using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.AllLinkArrows
{
    public class AllLinkArrowsQueryHandler : IRequestHandler<AllLinkArrowsQuery, IEnumerable<LinkArrowDto>>
    {
        private readonly ILinkArrowRepository _repository;

        public AllLinkArrowsQueryHandler(ILinkArrowRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LinkArrowDto>> Handle(AllLinkArrowsQuery request, CancellationToken cancellationToken)
        {
            var linkArrows = await _repository.AllLinkArrows();

            return Mapper.Map<IEnumerable<LinkArrowDto>>(linkArrows);
        }
    }
}