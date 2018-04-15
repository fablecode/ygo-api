using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ygo.application.Queries.FormatByAcronym;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class FormatsController : Controller
    {
        private readonly IMediator _mediator;

        public FormatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{acronym}")]
        public async Task<IActionResult> Get(string acronym)
        {
            var result = await _mediator.Send(new FormatByAcronymQuery {Acronym = acronym});

            return Ok(result);
        }
    }
}