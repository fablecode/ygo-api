using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands.AddCard;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class AddCardCommandHandlerTests
    {
        private AddCardCommandHandler _sut;
        private IMediator _mediator;

        [TestInitialize]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new AddCardCommandHandler(_mediator, new AddCardCommandValidator());
        }

        [TestMethod]
        public async Task Given_An_Invalid_AddCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new AddCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }
    }
}