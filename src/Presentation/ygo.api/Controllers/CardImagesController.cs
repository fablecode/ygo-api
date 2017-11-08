using Microsoft.AspNetCore.Mvc;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class CardImagesController : Controller
    {
        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            return StatusCode(501);
        }
    }
}