﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ygo.api.Auth;
using ygo.application.Commands.UpdateArchetypeCards;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class ArchetypeCardsController : Controller
    {
        private readonly IMediator _mediator;

        public ArchetypeCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put([FromBody] UpdateArchetypeCardsCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Errors);

        }
    }
}