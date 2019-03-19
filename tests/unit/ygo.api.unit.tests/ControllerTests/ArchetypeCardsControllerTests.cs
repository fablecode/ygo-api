using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.UpdateArchetypeCards;
using ygo.application.Dto;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeCardsControllerTests
    {
        private IMediator _mediator;
        private ArchetypeCardsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new ArchetypeCardsController(_mediator);
        }

        [Test]
        public async Task Given_An_Invalid_UpdateArchetypeCardsCommand_Should_Return_BadRequestResult()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeCardsCommand>()).Returns(new CommandResult());

            // Act
            var result = await _sut.Put(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_Valid_UpdateArchetypeCardsCommand_Should_Return_OkResult()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeCardsCommand>()).Returns(new CommandResult
            {
                IsSuccessful = true,
                Data = new List<ArchetypeCardDto> { new ArchetypeCardDto()}
            });

            // Act
            var result = await _sut.Put(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}