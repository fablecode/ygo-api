﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ygo.api.Auth;
using ygo.application.Commands.AddArchetype;
using ygo.application.Commands.UpdateArchetype;
using ygo.application.Queries.ArchetypeById;
using ygo.application.Queries.ArchetypeByName;

namespace ygo.api.Controllers
{
    public class ArchetypesController : Controller
    {
        private readonly IMediator _mediator;

        public ArchetypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Archetype by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _mediator.Send(new ArchetypeByIdQuery { Id = id});

            if (result != null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Archetype by 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _mediator.Send(new ArchetypeByNameQuery { Name = name });

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromBody] AddArchetypeCommand command)
        {
            var existingArchetype = await _mediator.Send(new ArchetypeByNameQuery { Name = command.Name });

            if (existingArchetype == null)
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                {
                    return CreatedAtRoute("ArchetypeById", new { id = result.Data }, result.Data);
                }

                return BadRequest(result.Errors);
            }

            return StatusCode((int)HttpStatusCode.Conflict, existingArchetype);
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put([FromBody] UpdateArchetypeCommand command)
        {
            var existingArchetype = await _mediator.Send(new ArchetypeByNameQuery { Name = command.Name });

            if (existingArchetype != null)
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Errors);
            }

            return NotFound(command.Name);
        }
    }
}