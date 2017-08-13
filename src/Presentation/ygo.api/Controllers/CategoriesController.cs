using System.Web.Http;

namespace ygo.api.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new []{"Spell", "Trap", "Monster"});
        }
    }
}