using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Queries.CardById;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.CardsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetCardByIdTests
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
        public async Task Given_A_Card_Id_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const int cardId = 523;

            // Act
            var result = await _sut.Get(cardId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public async Task Given_A_Card_Id_If_Found_Should_Return_OkResult()
        {
            // Arrange
            const int banlistId = 523;

            _mediator.Send(Arg.Any<CardByIdQuery>()).Returns(new CardDto());

            // Act
            var result = await _sut.Get(banlistId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Card_Id_If_Found_Should_Invoke_CardByIdQuery_Once()
        {
            // Arrange
            const int banlistId = 523;

            _mediator.Send(Arg.Any<CardByIdQuery>()).Returns(new CardDto());

            // Act
            await _sut.Get(banlistId);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<CardByIdQuery>());
        }
    }
}