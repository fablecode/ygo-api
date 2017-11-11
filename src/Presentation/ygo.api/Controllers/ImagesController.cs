using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ygo.application.Queries.CardImageByName;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("cards/{name}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _mediator.Send(new CardImageByNameQuery { Name = name });

            if(result.IsSuccessful)
                return new PhysicalFileResult(result.FilePath, result.ContentType);

            return NotFound();
        }
    }
}