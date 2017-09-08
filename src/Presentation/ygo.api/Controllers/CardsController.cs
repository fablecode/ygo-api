using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ygo.api.Auth;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.UpdateCard;
using ygo.application.Ioc;
using ygo.application.Queries.CardById;
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

        /// <summary>
        /// Card by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}", Name = "CardById")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _mediator.Send(new CardByIdQuery { Id = id });

            if (result != null)
                return Ok(result);

            return NotFound(id);
        }

        /// <summary>
        /// Card by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _mediator.Send(new CardByNameQuery {Name = name});

            if (result != null)
                return Ok(result);

            return NotFound(name);
        }



        [HttpGet]
        public IActionResult Get([FromRoute] CardSearchQuery query)
        {
            return StatusCode(501);
        }

        /// <summary>
        /// Add new card
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Post([FromBody] AddCardCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
                return CreatedAtRoute("CardById", new { id = ((CategoryDto)result.Data).Id }, result.Data);

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Update existing card
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = AuthConfig.SuperAdminsPolicy)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Put([FromBody] UpdateCardCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
                return Ok(result.Data);

            return BadRequest(result.Errors);
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

        [HttpGet("{id}/trivias")]
        public IActionResult GetTrivia()
        {
            return StatusCode(501);
        }

        [HttpPut("{id}/trivias")]
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