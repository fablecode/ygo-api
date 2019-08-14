using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.UpdateCard;
using ygo.application.Models.Cards.Input;
using ygo.application.Queries.CardById;
using ygo.application.Queries.CardByName;
using ygo.application.Queries.CardExists;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Card by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}", Name = "CardById")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _mediator.Send(new CardByIdQuery {Id = id});

            if (result != null)
                return Ok(result);

            return NotFound(id);
        }

        /// <summary>
        ///     Card by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _mediator.Send(new CardByNameQuery {Name = name});

            if (result != null)
                return Ok(result);

            return NotFound(name);
        }
    }
}