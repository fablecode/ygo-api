using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ygo.application.Enums;
using ygo.application.Queries.BanlistById;
using ygo.application.Queries.LatestBanlistByFormat;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class BanlistsController : Controller
    {
        private readonly IMediator _mediator;

        public BanlistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// The latest banlist based on format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        [HttpGet("latest/{format}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Latest([FromRoute] BanlistFormat format)
        {
            var result = await _mediator.Send(new LatestBanlistQuery {Acronym = format});

            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}