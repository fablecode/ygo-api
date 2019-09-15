using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.application.Queries;
using ygo.application.Queries.CardByName;
using ygo.application.Queries.CardSearch;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.CardsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetCardByNameTests
    {
        private IMediator _mediator;
        private CardsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new CardsController(_mediator);
        }

        [Test]
        public async Task Given_A_Card_Name_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const string cardName = "Call Of The Haunted";

            // Act
            var result = await _sut.Get(cardName);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public async Task Given_A_Card_Name_If_Found_Should_Return_OkResult()
        {
            // Arrange
            const string cardName = "Call Of The Haunted";

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns(new CardDto());

            // Act
            var result = await _sut.Get(cardName);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Card_Name_If_Found_Should_Invoke_CardByNameQuery_Once()
        {
            // Arrange
            const string cardName = "Call Of The Haunted";

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns(new CardDto());

            // Act
            await _sut.Get(cardName);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<CardByNameQuery>());
        }

    }

    [TestFixture]
    [Category(TestType.Unit)]
    public class GetCardSearchQueryTests
    {
        private IMediator _mediator;
        private CardsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new CardsController(_mediator);
        }

        [Test]
        public async Task Given_An_Invalid_CardSearchQuery_Should_Return_BadRequest()
        {
            // Arrange
            _mediator.Send(Arg.Any<CardSearchQuery>()).Returns(new QueryResult());

            var query = new CardSearchQuery
            {
                PageSize = 11
            };

            // Act
            var result = await _sut.Get(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_Invalid_CardSearchQuery_Should_Return_Errors()
        {
            // Arrange
            _mediator.Send(Arg.Any<CardSearchQuery>()).Returns(new QueryResult{ Errors = new List<string>{"PageSize must be 10 or less"}});
            var query = new CardSearchQuery
            {
                PageSize = 11
            };

            // Act
            var result = await _sut.Get(query) as BadRequestObjectResult; ;

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().NotBeNullOrEmpty();

        }

        [Test]
        public async Task Given_A_Valid_CardSearchQuery_But_Empty_List_Should_Return_NotFound()
        {
            // Arrange
            _mediator.Send(Arg.Any<CardSearchQuery>()).Returns(new QueryResult{ IsSuccessful = true, Data = new PagedList<CardDto>(new List<CardDto>(), 0, 1, 10)});
            var query = new CardSearchQuery
            {
                PageSize = 10
            };

            _sut.ControllerContext = new ControllerContext();
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await _sut.Get(query); ;

            // Assert
            result.Should().BeOfType<NotFoundResult>();

        }

        [Test]
        public async Task Given_A_Valid_CardSearchQuery_Should_Return_OkResult()
        {
            // Arrange
            _mediator.Send(Arg.Any<CardSearchQuery>()).Returns(new QueryResult
            {
                IsSuccessful = true,
                Data = new PagedList<CardDto>(new List<CardDto>
                {
                    new CardDto()
                }, 1, 1, 10)
            });
            var query = new CardSearchQuery
            {
                PageSize = 10
            };

            var controllerContext = new ControllerContext();
            var httpContext = new DefaultHttpContext();

            _sut.ControllerContext = controllerContext;
            _sut.ControllerContext.HttpContext = httpContext;

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("callback");

            _sut.Url = urlHelper;

            // Act
            var result = await _sut.Get(query); ;

            // Assert
            result.Should().BeOfType<OkObjectResult>();

        }
    }
}