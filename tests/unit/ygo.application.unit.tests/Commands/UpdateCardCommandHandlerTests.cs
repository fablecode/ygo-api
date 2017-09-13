using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands.UpdateCard;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class UpdateCardCommandHandlerTests
    {
        private UpdateCardCommandHandler _sut;
        private IMediator _mediator;

        [TestInitialize]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();
            IOptions<ApplicationSettings> settings = Substitute.For<IOptions<ApplicationSettings>>();

            _sut = new UpdateCardCommandHandler(_mediator, new UpdateCardCommandValidator(), settings);
        }

        [TestMethod]
        public async Task Given_An_Invalid_UpdateCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new UpdateCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }
    }
}