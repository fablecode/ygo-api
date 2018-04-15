using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ygo.application.Queries.ArchetypeImageById;
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

        /// <summary>
        /// Card image by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("cards/{name}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _mediator.Send(new CardImageByNameQuery {Name = name});

            if (result.IsSuccessful)
                return new PhysicalFileResult(result.FilePath, result.ContentType);

            return NotFound();
        }

        /// <summary>
        /// Archetype image by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("archetypes/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _mediator.Send(new ArchetypeImageByIdQuery { Id = id });

            if (result.IsSuccessful)
                return new PhysicalFileResult(result.FilePath, result.ContentType);

            return NotFound();
        }

    }
}