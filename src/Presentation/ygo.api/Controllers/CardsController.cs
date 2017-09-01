using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ygo.application.Commands.AddCard;
using ygo.application.Queries.CardByName;

namespace ygo.api.Controllers
{
    [Route("[controller]")]
    public class CardsController : Controller
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _mediator.Send(new CardByNameQuery {Name = name});

            if (result != null)
                return Ok(result);

            return NotFound($"Card '{name}' not found.");
        }

        [HttpGet]
        public IActionResult Get([FromRoute] CardSearchQuery query)
        {
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCardCommand command)
        {
            return StatusCode(501);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateCardCommand command)
        {
            return StatusCode(501);
        }


        [HttpGet("{id}/tips")]
        public IActionResult GetTips()
        {
            return StatusCode(501);
        }

        [HttpPut("{id}/tips")]
        public IActionResult PutTips([FromBody] UpdateCardTipsCommand command)
        {
            return StatusCode(501);
        }

        [HttpGet("{id}/rulings")]
        public IActionResult GetRulings()
        {
            return StatusCode(501);
        }

        [HttpPut("{id}/rulings")]
        public IActionResult PutRulings([FromBody] UpdateCardRulingsCommand command)
        {
            return StatusCode(501);
        }

        [HttpGet("{id}/trivia")]
        public IActionResult GetTrivia()
        {
            return StatusCode(501);
        }

        [HttpPut("{id}/trivia")]
        public IActionResult PutTrivia([FromBody] UpdateCardTriviaCommand command)
        {
            return StatusCode(501);
        }


        [HttpGet("{id}/linkarrows")]
        public IActionResult GetLinkArrows()
        {
            return StatusCode(501);
        }

        [HttpPut("{id}/linkarrows")]
        public IActionResult PutLinkArrows([FromBody] UpdateCardLinkArrowsCommand command)
        {
            return StatusCode(501);
        }
    }

    public class UpdateCardCommand
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }

        public Uri ImageUrl { get; set; }

        public List<string> SubCategories { get; set; }
        public List<string> Types { get; set; }
        public List<string> LinkArrows { get; set; }
    }

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

    public class UpdateCardLinkArrowsCommand
    {
        public long Id { get; set; }
        public List<string> LinkArrows { get; set; }
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

    public class UpdateCardImageCommand
    {
        public long Id { get; set; }
        public string ImageUrl{ get; set; }
    }

    public class UpdateCardTriviaCommand
    {
        public long Id { get; set; }
        public List<string> Trivia { get; set; }
    }

    public class UpdateCardRulingsCommand
    {
        public long Id { get; set; }
        public List<string> Rulings { get; set; }
    }

    public class UpdateCardTipsCommand
    {
        public long Id { get; set; }
        public List<string> Tips { get; set; }
    }
}