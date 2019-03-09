using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.core.Services;

namespace ygo.application.Queries.ArchetypeSearch
{
    public class ArchetypeSearchQueryHandler : IRequestHandler<ArchetypeSearchQuery, QueryResult>
    {
        private readonly IArchetypeService _archetypeService;
        private readonly IValidator<ArchetypeSearchQuery> _validator;

        public ArchetypeSearchQueryHandler(IArchetypeService archetypeService, IValidator<ArchetypeSearchQuery> validator)
        {
            _archetypeService = archetypeService;
            _validator = validator;
        }

        public async Task<QueryResult> Handle(ArchetypeSearchQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var searchResult = await _archetypeService.Search(request.SearchTerm, request.PageNumber, request.PageSize);

                var archetypeList = searchResult.Items.Select(a => a.MapToArchetypeDto()).ToList();

                queryResult.Data = new PagedList<ArchetypeDto>(archetypeList, searchResult.TotalRecords, request.PageNumber, request.PageSize);
                queryResult.IsSuccessful = true;
            }
            else
            {
                queryResult.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return queryResult;
        }
    }

}