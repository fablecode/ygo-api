using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ygo.api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        [HttpGet("{email}", Name = "UserByEmail")]
        public Task<IActionResult> Get(string email)
        {
            throw new NotImplementedException();
        }
    }
}