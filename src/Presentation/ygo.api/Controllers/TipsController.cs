using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.UpdateTips;

namespace ygo.api.Controllers
{
    [Route("api/cards/{cardId}/[controller]")]
    public class TipsController : Controller
    {
        private readonly IMediator _mediator;

        public TipsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public async Task<IActionResult> Put([FromBody] UpdateTipsCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.Errors);
        }
    }
}