using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ygo.application.Ioc;
using ygo.application.Repository;

namespace ygo.application.Queries.AllLinkArrows
{
    public class AllLinkArrowsQueryHandler : IAsyncRequestHandler<AllLinkArrowsQuery, IEnumerable<LinkArrowDto>>
    {
        private readonly ILinkArrowRepository _repository;

        public AllLinkArrowsQueryHandler(ILinkArrowRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LinkArrowDto>> Handle(AllLinkArrowsQuery message)
        {
            var linkArrows = await _repository.AllLinkArrows();

            return Mapper.Map<IEnumerable<LinkArrowDto>>(linkArrows);
        }
    }
}