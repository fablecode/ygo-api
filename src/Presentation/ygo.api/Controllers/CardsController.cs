﻿using System.Net;
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

        /// <summary>
        ///     Add new card
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CardInputModel inputModel)
        {
            var existingCard = await _mediator.Send(new CardByNameQuery {Name = inputModel.Name});

            if (existingCard == null)
            {
                var command = new AddCardCommand{ Card = inputModel };

                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                    return CreatedAtRoute("CardById", new {id = result.Data}, result.Data);

                return BadRequest(result.Errors);
            }

            return StatusCode((int) HttpStatusCode.Conflict);
        }

        /// <summary>
        ///     Update existing card
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put([FromBody] CardInputModel inputModel)
        {
            var cardExists = await _mediator.Send(new CardExistsQuery {Id = inputModel.Id});

            if (cardExists)
            {
                var command = new UpdateCardCommand{ Card = inputModel};

                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                    return Ok(result.Data);

                return BadRequest(result.Errors);
            }

            return NotFound(inputModel.Id);
        }
    }
}