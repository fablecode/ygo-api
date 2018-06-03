﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.UpdateTrivias;

namespace ygo.api.Controllers
{
    [Route("api/cards/{cardId}/[controller]")]
    public class TriviaController : Controller
    {
        private readonly IMediator _mediator;

        public TriviaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get(long cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public async Task<IActionResult> PutTrivia([FromBody] UpdateTriviaCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.Errors);
        }
    }
}