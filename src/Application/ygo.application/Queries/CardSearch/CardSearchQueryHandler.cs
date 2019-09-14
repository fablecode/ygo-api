using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.core.Services;

namespace ygo.application.Queries.CardSearch
{
    public class CardSearchQueryHandler : IRequestHandler<CardSearchQuery, QueryResult>
    {
        private readonly ICardService _cardService;
        private readonly IValidator<CardSearchQuery> _validator;
        private readonly IMapper _mapper;

        public CardSearchQueryHandler(ICardService cardService, IValidator<CardSearchQuery> validator, IMapper mapper)
        {
            _cardService = cardService;
            _validator = validator;
            _mapper = mapper;
        }


        public async Task<QueryResult> Handle(CardSearchQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new QueryResult();

            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                var searchResult = await _cardService.Search(request.SearchTerm, request.PageIndex, request.PageSize);

                var cardList = searchResult.Items.Select(c => _mapper.Map<CardDto>(c)).ToList();

                queryResult.Data = new PagedList<CardDto>(cardList, searchResult.TotalRecords, request.PageIndex, request.PageSize);
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