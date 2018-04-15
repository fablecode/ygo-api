using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ygo.application.Queries.AllSubCategories;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class SubCategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public SubCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     All SubCategories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new AllSubCategoriesQuery());

            return Ok(result);
        }
    }
}