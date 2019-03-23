using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.UpdateTrivia;
using ygo.application.Dto;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class TriviasControllerTests
    {
        private IMediator _mediator;
        private TriviaController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new TriviaController(_mediator);
        }

        [Test]
        public async Task Given_A_UpdateTriviaCommand_If_Update_Fails_Should_Return_BadRequestResult()
        {
            // Arrange
            var command = new UpdateTriviaCommand();

            _mediator.Send(Arg.Any<UpdateTriviaCommand>()).Returns(new CommandResult());

            // Act
            var result = await _sut.Put(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_A_UpdateTriviaCommand_If_Update_Fails_Should_Return_BadRequestResult_With_Errors()
        {
            // Arrange
            const string expected = "Card id must be greater than 0.";

            var command = new UpdateTriviaCommand();

            _mediator.Send(Arg.Any<UpdateTriviaCommand>()).Returns(new CommandResult { Errors = new List<string> { "Card id must be greater than 0." } });

            // Act
            var result = await _sut.Put(command) as BadRequestObjectResult;

            // Assert
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }


        [Test]
        public async Task Given_A_UpdateTriviaCommand_If_Update_Succeeds_Should_Return_OkResult()
        {
            // Arrange
            var command = new UpdateTriviaCommand { CardId = 3432, Trivia = new List<TriviaSectionDto>() };

            _mediator.Send(Arg.Any<UpdateTriviaCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            var result = await _sut.Put(command);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task Given_A_UpdateTriviaCommand_If_Update_Succeeds_Should_Invoke_UpdateTriviaCommand_Once()
        {
            // Arrange
            var command = new UpdateTriviaCommand { CardId = 3432, Trivia = new List<TriviaSectionDto>() };

            _mediator.Send(Arg.Any<UpdateTriviaCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            await _sut.Put(command);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<UpdateTriviaCommand>());
        }
    }
}