﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using ygo.api.Auth;
using ygo.application.Commands.AddArchetype;
using ygo.application.Commands.UpdateArchetypeCards;
using ygo.application.Queries.ArchetypeById;
using ygo.application.Queries.ArchetypeByName;
using ygo.application.Queries.ArchetypeSearch;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id:long}", Name = "ArchetypeById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _mediator.Send(new ArchetypeByIdQuery { Id = id});

            if (result != null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Archetype by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("names")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _mediator.Send(new ArchetypeByNameQuery { Name = name });

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Archetype list with pagination.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public Task<IActionResult> GetArchetypeList([FromQuery]ArchetypeSearchQuery query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new Archetype
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromBody] AddArchetypeCommand command)
        {
            var existingArchetype = await _mediator.Send(new ArchetypeByIdQuery { Id = command.ArchetypeNumber });

            if (existingArchetype == null)
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                {
                    return CreatedAtRoute("ArchetypeById", new { id = result.Data }, result.Data);
                }

                return BadRequest(result.Errors);
            }

            return StatusCode((int)HttpStatusCode.Conflict);
        }

        /// <summary>
        /// Update an existing Archetype
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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