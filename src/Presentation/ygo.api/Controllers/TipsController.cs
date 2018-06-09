using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ygo.api.Auth;
using ygo.application.Commands.UpdateTips;

namespace ygo.api.Controllers
{
    [Route("api/cards/{cardId}/[controller]")]
    public class TipsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TipsController> _logger;

        public TipsController(IMediator mediator, ILogger<TipsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int cardId)
        {
            return StatusCode(501);
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        public async Task<IActionResult> Put([FromBody] UpdateTipsCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccessful)
                    return Ok();

                return BadRequest(result.Errors);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Update cards tips");
                throw;
            }
        }
    }
}