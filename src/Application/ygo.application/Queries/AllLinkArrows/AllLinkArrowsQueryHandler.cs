using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.Queries.AllLinkArrows
{
    public class AllLinkArrowsQueryHandler : IAsyncRequestHandler<AllLinkArrowsQuery, IEnumerable<LinkArrow>>
    {
        private readonly ILinkArrowRepository _repository;

        public AllLinkArrowsQueryHandler(ILinkArrowRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LinkArrow>> Handle(AllLinkArrowsQuery message)
        {
            return await _repository.AllLinkArrows();
        }
    }
}