using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ygo.api.Constants;
using ygo.api.Model;
using ygo.api.ServiceExtensions;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.application.Queries.CardById;
using ygo.application.Queries.CardByName;
using ygo.application.Queries.CardSearch;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private const string CardSearchRouteName = "CardSearch";

        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Card by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}", Name = "CardById")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _mediator.Send(new CardByIdQuery {Id = id});

            if (result != null)
                return Ok(result);

            return NotFound(id);
        }

        /// <summary>
        ///     Card by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _mediator.Send(new CardByNameQuery {Name = name});

            if (result != null)
                return Ok(result);

            return NotFound(name);
        }

        /// <summary>
        ///     Paginated card list
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet(Name = CardSearchRouteName)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] CardSearchQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.IsSuccessful)
            {
                var searchResults = (PagedList<CardDto>)result.Data;

                Response.Headers.Add(HttpHeaderConstants.XPagination, searchResults.GetHeader().ToJson());

                if (searchResults.List.Any())
                {
                    return Ok(new
                    {
                        Links = CardSearchLinks(searchResults, query.SearchTerm),
                        Items = searchResults.List
                    });
                }

                return NotFound();
            }

            return BadRequest(result.Errors);

        }

        #region private helpers

        private List<LinkInfo> CardSearchLinks(PagedList<CardDto> list, string searchTerm)
        {
            var links = new List<LinkInfo>();


            if (list.HasPreviousPage)
                links.Add(CardSearchCreateLink(CardSearchRouteName, searchTerm, list.PreviousPageNumber,
                    list.PageSize, "previous", "GET"));

            links.Add(CardSearchCreateLink(CardSearchRouteName, searchTerm, list.PageIndex, list.PageSize,
                "self", "GET"));

            if (list.HasNextPage)
                links.Add(CardSearchCreateLink(CardSearchRouteName, searchTerm, list.NextPageNumber,
                    list.PageSize, "next", "GET"));

            return links;
        }

        private LinkInfo CardSearchCreateLink(string routeName, string searchTerm, int pageNumber, int pageSize,
            string rel, string method)
        {
            var values = new { SearchTerm = searchTerm, PageIndex = pageNumber, PageSize = pageSize };

            return new LinkInfo
            {
                Href = Url.Link(routeName, values),
                Rel = rel,
                Method = method
            };
        } 

        #endregion
    }
}