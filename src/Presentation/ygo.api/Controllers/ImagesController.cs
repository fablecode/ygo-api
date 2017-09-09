using Microsoft.AspNetCore.Mvc;
using ygo.application.Commands.UpdateCardImage;

namespace ygo.api.Controllers
{
    [Route("[controller]")]
    public class ImagesController : Controller
    {
        [HttpGet("{id}/image")]
        public IActionResult GetImage()
        {
            return StatusCode(501);
        }

        [HttpPost("{id}/image")]
        public IActionResult PostImage([FromBody] UpdateCardImageCommand command)
        {
            return StatusCode(501);
        }


        [HttpPut("{id}/image")]
        public IActionResult PutImage([FromBody] UpdateCardImageCommand command)
        {
            return StatusCode(501);
        }
    }
}