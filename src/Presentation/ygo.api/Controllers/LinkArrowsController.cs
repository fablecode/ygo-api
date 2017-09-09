using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ygo.application.Queries.AllLinkArrows;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class LinkArrowsController : Controller
    {
        private readonly IMediator _mediator;

        public LinkArrowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new AllLinkArrowsQuery());

            return Ok(result);
        }
    }
}