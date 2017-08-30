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
        public Task<IActionResult> Get([FromRoute] CardSearchQuery query)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Post([FromBody] AddCardCommand command)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/tips")]
        public Task<IActionResult> GetTips()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}/tips")]
        public Task<IActionResult> PutTips([FromBody] UpdateCardTipsCommand command)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/rulings")]
        public Task<IActionResult> GetRulings()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}/rulings")]
        public Task<IActionResult> PutRulings([FromBody] UpdateCardRulingsCommand command)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/trivia")]
        public Task<IActionResult> GetTrivia()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}/trivia")]
        public Task<IActionResult> PutTrivia([FromBody] UpdateCardTriviaCommand command)
        {
            throw new NotImplementedException();
        }


        [HttpGet("{id}/linkarrows")]
        public Task<IActionResult> GetLinkArrows()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}/linkarrows")]
        public Task<IActionResult> PutLinkArrows([FromBody] UpdateCardLinkArrowsCommand command)
        {
            throw new NotImplementedException();
        }


        [HttpGet("{id}/image")]
        public Task<IActionResult> GetImage()
        {
            throw new NotImplementedException();
        }


        [HttpPut("{id}/image")]
        public Task<IActionResult> PutImage([FromBody] UpdateCardImageCommand command)
        {
            throw new NotImplementedException();
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