using AutoMapper;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.domain.Repository;

namespace ygo.application.Queries.FormatByAcronym
{
    public class FormatByAcronymQueryHandler : IAsyncRequestHandler<FormatByAcronymQuery, FormatDto>
    {
        private readonly IFormatRepository _repository;
        private readonly IValidator<FormatByAcronymQuery> _validator;

        public FormatByAcronymQueryHandler(IFormatRepository repository, IValidator<FormatByAcronymQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<FormatDto> Handle(FormatByAcronymQuery message)
        {
            var validatorResults = _validator.Validate(message);

            if (validatorResults.IsValid)
            {
                var result = await _repository.FormatByAcronym(message.Acronym);

                if (result != null)
                    return Mapper.Map<FormatDto>(result);
            }

            return null;
        }
    }
}