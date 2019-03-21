using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Queries.CardByName;
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
}