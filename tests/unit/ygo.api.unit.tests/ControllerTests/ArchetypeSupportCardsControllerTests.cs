using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.UpdateArchetypeSupportCards;
using ygo.application.Dto;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeSupportCardsControllerTests
    {
        private IMediator _mediator;
        private ArchetypeSupportCardsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new ArchetypeSupportCardsController(_mediator);
        }

        [Test]
        public async Task Given_An_UpdateArchetypeSupportCardsCommand_If_Command_Is_Not_Successful_Should_Return_BadRequest()
        {
            // Arrange
            var query = new UpdateArchetypeSupportCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeSupportCardsCommand>()).Returns(new CommandResult());

            // Act
            var result = await _sut.Put(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_UpdateArchetypeSupportCardsCommand_If_Command_Is_Successful_Should_Return_BadRequest()
        {
            // Arrange
            var query = new UpdateArchetypeSupportCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeSupportCardsCommand>()).Returns(new CommandResult { IsSuccessful = true, Data = new List<ArchetypeDto>() });

            // Act
            var result = await _sut.Put(query);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_An_UpdateArchetypeCardsCommand_If_Command_Is_Successful_Should_Invoke_UpdateArchetypeCardsCommand_Once()
        {
            // Arrange
            var query = new UpdateArchetypeSupportCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeSupportCardsCommand>()).Returns(new CommandResult { IsSuccessful = true, Data = new List<ArchetypeDto>() });

            // Act
            await _sut.Put(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<UpdateArchetypeSupportCardsCommand>());
        }
    }
}