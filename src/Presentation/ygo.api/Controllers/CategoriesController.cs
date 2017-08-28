using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.application.Commands.AddCategory;
using ygo.application.Queries.AllCategories;
using ygo.application.Queries.CategoryById;
using ygo.domain.Models;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// All categories ordered alphabetically
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new AllCategoriesQuery());

            return Ok(result);
        }

        /// <summary>
        /// Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "CategoryById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new CategoryByIdQuery {Id = id});

            if (result == null)
                return NotFound($"Category with id '{id}' not found.");

            return Ok(result);
        }

        /// <summary>
        /// New category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromBody] AddCategoryCommand category)
        {
            var result = await _mediator.Send(category);

            if (result.IsSuccessful)
                return CreatedAtRoute("CategoryById", new { id = ((Category)result.Data).Id }, result.Data);

            return BadRequest(result.Errors);
        }
    }
}
