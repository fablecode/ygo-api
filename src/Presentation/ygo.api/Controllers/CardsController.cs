using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ygo.api.Controllers
{
    [Route("[controller]")]
    public class CardsController : Controller
    {
        [HttpGet("{name}")]
        public Task<IActionResult> Get(string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<IActionResult> Get([FromRoute] CardSearchQuery query)
        {
            throw new NotImplementedException();
        }


    }

    public class CardSearchQuery
    {
        public long BanlistId { get; set; }
        public long LimitId { get; set; }
        public long CategoryId { get; set; }
        public IEnumerable<long> SubCategoryIds { get; set; }
        public long AttributeId { get; set; }
        public long TypeId { get; set; }
        public int LvlRank { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string SearchText { get; set; }

        [Range(1, 10)]
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

    }

    [Route("[controller]")]
    public class ArchetypesController : Controller
    {
        [HttpGet("{id}")]
        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<IActionResult> Get([FromRoute] ArchetypeSearchQuery query)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/cards")]
        public Task<IActionResult> Cards(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{id}/cards")]
        public Task<IActionResult> Cards([FromBody]ArchetypeAddCardsCommand command)
        {
            throw new NotImplementedException();
        }

    }

    public class ArchetypeAddCardsCommand
    {
        public long Id { get; set; }

        public IEnumerable<long> Cards { get; set; }
    }

    public class ArchetypeSearchQuery
    {
        public string SearchText { get; set; }

        public int PageSize { get; set; } = 12;

        public int PageIndex { get; set; }
    }
}