using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.UpdateTips;
using ygo.application.Dto;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class TipsControllerTests
    {
        private IMediator _mediator;
        private TipsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new TipsController(_mediator);
        }

        [Test]
        public async Task Given_A_UpdateTipCommand_If_Update_Fails_Should_Return_BadRequestResult()
        {
            // Arrange
            var command = new UpdateTipsCommand();

            _mediator.Send(Arg.Any<UpdateTipsCommand>()).Returns(new CommandResult());

            // Act
            var result = await _sut.Put(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_A_UpdateTipsCommand_If_Update_Fails_Should_Return_BadRequestResult_With_Errors()
        {
            // Arrange
            const string expected = "Card id must be greater than 0.";

            var command = new UpdateTipsCommand();

            _mediator.Send(Arg.Any<UpdateTipsCommand>()).Returns(new CommandResult { Errors = new List<string> { "Card id must be greater than 0." } });

            // Act
            var result = await _sut.Put(command) as BadRequestObjectResult;

            // Assert
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }


        [Test]
        public async Task Given_A_UpdateTipsCommand_If_Update_Succeeds_Should_Return_OkResult()
        {
            // Arrange
            var command = new UpdateTipsCommand { CardId = 3432, Tips = new List<TipSectionDto>() };

            _mediator.Send(Arg.Any<UpdateTipsCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            var result = await _sut.Put(command);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task Given_A_UpdateTipsCommand_If_Update_Succeeds_Should_Invoke_UpdateTipsCommand_Once()
        {
            // Arrange
            var command = new UpdateTipsCommand { CardId = 3432, Tips = new List<TipSectionDto>() };

            _mediator.Send(Arg.Any<UpdateTipsCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            await _sut.Put(command);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<UpdateTipsCommand>());
        }
    }
}