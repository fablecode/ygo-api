using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.UpdateRulings;
using ygo.application.Dto;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class RulingsControllerTests
    {
        private IMediator _mediator;
        private RulingsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new RulingsController(_mediator);
        }

        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Update_Fails_Should_Return_BadRequestResult()
        {
            // Arrange
            var command = new UpdateRulingCommand();

            _mediator.Send(Arg.Any<UpdateRulingCommand>()).Returns(new CommandResult());

            // Act
            var result = await _sut.PutRulings(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Update_Fails_Should_Return_BadRequestResult_With_Errors()
        {
            // Arrange
            const string expected = "Card id must be greater than 0.";

            var command = new UpdateRulingCommand();

            _mediator.Send(Arg.Any<UpdateRulingCommand>()).Returns(new CommandResult{ Errors = new List<string>{"Card id must be greater than 0."}});

            // Act
            var result = await _sut.PutRulings(command) as BadRequestObjectResult;

            // Assert
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }


        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Update_Succeeds_Should_Return_OkResult()
        {
            // Arrange
            var command = new UpdateRulingCommand{ CardId = 3432, Rulings = new List<RulingSectionDto>()};

            _mediator.Send(Arg.Any<UpdateRulingCommand>()).Returns(new CommandResult{ IsSuccessful = true});

            // Act
            var result = await _sut.PutRulings(command);

            // Assert
            result.Should().BeOfType<OkResult>();
        }


        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Update_Succeeds_Should_Invoke_UpdateRulingCommand_Once()
        {
            // Arrange
            var command = new UpdateRulingCommand{ CardId = 3432, Rulings = new List<RulingSectionDto>()};

            _mediator.Send(Arg.Any<UpdateRulingCommand>()).Returns(new CommandResult{ IsSuccessful = true});

            // Act
            await _sut.PutRulings(command);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<UpdateRulingCommand>());
        }
    }
}