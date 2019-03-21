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

namespace ygo.api.unit.tests.ControllerTests.ArchetypesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class PutTests
    {
        private IMediator _mediator;
        private ArchetypesController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new ArchetypesController(_mediator);
        }

        [Test]
        public async Task Given_An_UpdateArchetypeCardsCommand_If_Command_Is_Not_Successful_Should_Return_BadRequest()
        {
            // Arrange
            var query = new UpdateArchetypeCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeCardsCommand>()).Returns(new CommandResult());

            // Act
            var result = await _sut.Put(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_UpdateArchetypeCardsCommand_If_Command_Is_Successful_Should_Return_BadRequest()
        {
            // Arrange
            var query = new UpdateArchetypeCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeCardsCommand>()).Returns(new CommandResult{ IsSuccessful = true, Data = new ArchetypeDto()});

            // Act
            var result = await _sut.Put(query);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }


        [Test]
        public async Task Given_An_UpdateArchetypeCardsCommand_If_Command_Is_Successful_Should_Invoke_UpdateArchetypeCardsCommand_Once()
        {
            // Arrange
            var query = new UpdateArchetypeCardsCommand();

            _mediator.Send(Arg.Any<UpdateArchetypeCardsCommand>()).Returns(new CommandResult{ IsSuccessful = true, Data = new ArchetypeDto()});

            // Act
            await _sut.Put(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<UpdateArchetypeCardsCommand>());
        }
    }
}