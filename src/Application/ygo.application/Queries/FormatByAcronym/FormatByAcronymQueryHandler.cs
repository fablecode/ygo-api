using System.Threading;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.FormatByAcronym
{
    public class FormatByAcronymQueryHandler : IRequestHandler<FormatByAcronymQuery, FormatDto>
    {
        private readonly IFormatRepository _repository;

        public FormatByAcronymQueryHandler(IFormatRepository repository)
        {
            _repository = repository;
        }

        public async Task<FormatDto> Handle(FormatByAcronymQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.FormatByAcronym(request.Acronym.ToString());

            return result != null ? Mapper.Map<FormatDto>(result) : null;
        }
    }
}