using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ygo.application.Queries.AllLimits;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class LimitsController : Controller
    {
        private readonly IMediator _mediator;

        public LimitsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new AllLimitsQuery());

            return Ok(result);
        }
    }
}