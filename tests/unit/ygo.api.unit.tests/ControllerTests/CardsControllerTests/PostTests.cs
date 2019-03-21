using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.AddCard;
using ygo.application.Dto;
using ygo.application.Models.Cards.Input;
using ygo.application.Queries.CardByName;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.CardsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class PostTests
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

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns(new CardDto());

            // Act
            var result = await _sut.Post(cardInputModel);

            // Assert
            result.Should().BeOfType<StatusCodeResult>();
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Not_Found_Validation_Failed_Should_Return_BadRequest()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns((CardDto) null);
            _mediator.Send(Arg.Any<AddCardCommand>()).Returns(new CommandResult { Errors = new List<string>{ "CardType must have a value."}});

            // Act
            var result = await _sut.Post(cardInputModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Test]
        public async Task Given_CardInputModel_If_Card_Not_Found_Validation_Failed_Should_Return_BadRequest_With_Errors()
        {
            // Arrange
            const string expected = "CardType must have a value.";
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns((CardDto) null);
            _mediator.Send(Arg.Any<AddCardCommand>()).Returns(new CommandResult { Errors = new List<string>{ "CardType must have a value."}});

            // Act
            var result = await _sut.Post(cardInputModel) as BadRequestObjectResult;

            // Assert
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Not_Found_Validation_Pass_Should_Return_OkResult()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns((CardDto) null);
            _mediator.Send(Arg.Any<AddCardCommand>()).Returns(new CommandResult { IsSuccessful = true});

            // Act
            var result = await _sut.Post(cardInputModel);

            // Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Not_Found_Validation_Pass_Should_Invoke_CardByNameQuery_Once()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns((CardDto) null);
            _mediator.Send(Arg.Any<AddCardCommand>()).Returns(new CommandResult { IsSuccessful = true});

            // Act
            await _sut.Post(cardInputModel);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<CardByNameQuery>());
        }

        [Test]
        public async Task Given_CardInputModel_If_Card_Not_Found_Validation_Pass_Should_Invoke_AddCardCommand_Once()
        {
            // Arrange
            var cardInputModel = new CardInputModel
            {
                Name = "Call Of The Haunted"
            };

            _mediator.Send(Arg.Any<CardByNameQuery>()).Returns((CardDto) null);
            _mediator.Send(Arg.Any<AddCardCommand>()).Returns(new CommandResult { IsSuccessful = true});

            // Act
            await _sut.Post(cardInputModel);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<AddCardCommand>());
        }
    }
}