using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;

namespace ygo.api.Controllers
{
    public class ArchetypesController : Controller
    {
        /// <summary>
        /// Archetype by 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public IActionResult Get(string name)
        {
            return StatusCode(501);
        }

        [HttpPost]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public IActionResult Post([FromBody] AddArchetypeCommand command)
        {
            return StatusCode(501);
        }

        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Put([FromBody] UpdateArchetypeCommand command)
        {
            return StatusCode(501);
        }

        [HttpPut("{id:long}/cards")]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        public IActionResult Put(long id, [FromBody] UpdateArchetypeCardsCommand command)
        {
            return StatusCode(501);
        }

    }

    public class UpdateArchetypeCardsCommand
    {
    }

    public class UpdateArchetypeCommand
    {
    }

    public class AddArchetypeCommand
    {
    }
}