using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.UpdateCard;
using ygo.application.Models.Cards.Input;
using ygo.application.Queries.CardExists;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.CardsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class PutTests
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
        public async Task Given_CardInputModel_If_Card_Exists_Should_Return_StatusCode_Confict()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardExistsQuery>()).Returns(false);

            // Act
            var result = await _sut.Put(cardInputModel);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Exists_But_Fails_Validation_Should_Return_BadRequest()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateCardCommand>()).Returns(new CommandResult { Errors = new List<string> { "CardType must have a value." } });

            // Act
            var result = await _sut.Put(cardInputModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Exists_And_Passes_Validation_Should_Return_OkResult()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateCardCommand>()).Returns(new CommandResult { IsSuccessful = true});

            // Act
            var result = await _sut.Put(cardInputModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Not_Found_Validation_Pass_Should_Invoke_CardExistsQuery_Once()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateCardCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            await _sut.Put(cardInputModel);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<CardExistsQuery>());
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Not_Found_Validation_Pass_Should_Invoke_UpdateCardCommand_Once()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateCardCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            await _sut.Put(cardInputModel);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<UpdateCardCommand>());
        }
    }
}