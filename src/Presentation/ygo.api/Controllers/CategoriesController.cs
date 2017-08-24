using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ygo.api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
