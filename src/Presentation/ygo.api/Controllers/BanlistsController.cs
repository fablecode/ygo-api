﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ygo.api.Auth;
using ygo.application.Commands.AddBanlist;
using ygo.application.Commands.UpdateBanlist;
using ygo.application.Commands.UpdateBanlistCards;
using ygo.application.Queries.BanlistById;
using ygo.application.Queries.BanlistExists;

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
        /// Banlist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}", Name = "BanlistById")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _mediator.Send(new BanlistByIdQuery { Id = id });

            if (result != null)
                return Ok(result);

            return NotFound(id);
        }


        /// <summary>
        /// Add new banlist
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromBody] AddBanlistCommand command)
        {
            var existingCard = await _mediator.Send(new BanlistByIdQuery { Id = command.Id });

            if (existingCard == null)
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                {
                    return CreatedAtRoute("BanlistById", new { id = result.Data }, result.Data);
                }

                return BadRequest(result.Errors);
            }

            return StatusCode((int)HttpStatusCode.Conflict, existingCard);
        }

        /// <summary>
        /// Update existing banlist
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put([FromBody] UpdateBanlistCommand command)
        {
            var banlistExists = await _mediator.Send(new BanlistExistsQuery { Id = command.Id });

            if (banlistExists)
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                    return Ok(result.Data);

                return BadRequest(result.Errors);
            }

            return NotFound(command.Id);
        }

        /// <summary>
        /// Update banlist cards
        /// </summary>
        /// <param name="banlistId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{banlistId:long}/cards")]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put(long banlistId, [FromBody] UpdateBanlistCardsCommand command)
        {
            var banlistExists = await _mediator.Send(new BanlistExistsQuery { Id = banlistId });

            if (banlistExists)
            {
                command.BanlistId = banlistId;

                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                    return Ok(result.Data);

                return BadRequest(result.Errors);
            }

            return NotFound(banlistId);
        }

    }
}