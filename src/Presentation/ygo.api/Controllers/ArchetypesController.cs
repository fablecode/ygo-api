using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using ygo.api.Auth;
using ygo.api.Model;
using ygo.application.Commands.AddArchetype;
using ygo.application.Commands.UpdateArchetypeCards;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.application.Queries.ArchetypeAutosuggest;
using ygo.application.Queries.ArchetypeById;
using ygo.application.Queries.ArchetypeByName;
using ygo.application.Queries.ArchetypeSearch;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class ArchetypesController : Controller
    {
        private const string ArchetypeSearchRouteName = "ArchetypeSearch";
        public const string XPagination = "X-Pagination";
        private readonly IMediator _mediator;

        public ArchetypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Archetype by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}", Name = "ArchetypeById")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _mediator.Send(new ArchetypeByIdQuery {Id = id});

            if (result != null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        ///     Archetype by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("named")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName([FromQuery] string name = "")
        {
            var result = await _mediator.Send(new ArchetypeByNameQuery {Name = name});

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        ///     Archeytpe autosuggest by name
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("names")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> AutoSuggests([FromQuery] string filter = "")
        {
            var result = await _mediator.Send(new ArchetypeAutosuggestQuery {Filter = filter});

            if (result != null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        ///     Paginated Archetype list
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet(Name = "ArchetypeSearch")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetArchetypeSearch([FromQuery] ArchetypeSearchQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.IsSuccessful)
            {
                var searchResults = (PagedList<ArchetypeDto>) result.Data;

                Response.Headers.Add(XPagination, searchResults.GetHeader().ToJson());

                if (searchResults.List.Any())
                {
                    var archetypeList = searchResults.List.ToList();

                    GenerateArchetypeImageLinks(archetypeList);

                    return Ok(new
                    {
                        Links = ArchetypeSearchLinks(searchResults, query.SearchTerm),
                        Items = archetypeList
                    });
                }

                return NotFound();
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        ///     Add a new Archetype
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromBody] AddArchetypeCommand command)
        {
            var existingArchetype = await _mediator.Send(new ArchetypeByIdQuery {Id = command.ArchetypeNumber});

            if (existingArchetype == null)
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                    return CreatedAtRoute("ArchetypeById", new {id = result.Data}, result.Data);

                return BadRequest(result.Errors);
            }

            return StatusCode((int) HttpStatusCode.Conflict);
        }

        /// <summary>
        ///     Update an existing Archetype
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put([FromBody] UpdateArchetypeCardsCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
                return Ok(result.Data);

            return BadRequest(result.Errors);
        }

        #region private helpers

        private List<LinkInfo> ArchetypeSearchLinks(PagedList<ArchetypeDto> list, string searchTerm)
        {
            var links = new List<LinkInfo>();


            if (list.HasPreviousPage)
                links.Add(ArchetypeSearchCreateLink(ArchetypeSearchRouteName, searchTerm, list.PreviousPageNumber,
                    list.PageSize, "previous", "GET"));

            links.Add(ArchetypeSearchCreateLink(ArchetypeSearchRouteName, searchTerm, list.PageNumber, list.PageSize,
                "self", "GET"));

            if (list.HasNextPage)
                links.Add(ArchetypeSearchCreateLink(ArchetypeSearchRouteName, searchTerm, list.NextPageNumber,
                    list.PageSize, "next", "GET"));

            return links;
        }

        private LinkInfo ArchetypeSearchCreateLink(string routeName, string searchTerm, int pageNumber, int pageSize,
            string rel, string method)
        {
            var values = new {SearchTerm = searchTerm, PageNumber = pageNumber, PageSize = pageSize};

            return new LinkInfo
            {
                Href = Url.Link(routeName, values),
                Rel = rel,
                Method = method
            };
        }

        private void GenerateArchetypeImageLinks(List<ArchetypeDto> archetypeList)
        {
            archetypeList.ForEach(a => { a.ImageUrl = Url.Link(ImagesController.ArchetypeImageRouteName, new { a.Id }); });
        }

        #endregion
    }
}