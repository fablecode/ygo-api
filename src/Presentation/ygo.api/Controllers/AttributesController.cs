using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ygo.application.Queries.AllAttributes;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class AttributesController : Controller
    {
        private readonly IMediator _mediator;

        public AttributesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new AllAttributesQuery());

            return Ok(result);
        }
    }
}