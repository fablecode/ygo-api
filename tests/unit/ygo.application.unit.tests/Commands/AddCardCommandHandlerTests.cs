using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using NUnit.Framework;
using ygo.application.Commands.AddCard;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class AddCardCommandHandlerTests
    {
        private AddCardCommandHandler _sut;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            IOptions<ApplicationSettings> settings = Substitute.For<IOptions<ApplicationSettings>>();

            _sut = new AddCardCommandHandler(_mediator, new AddCardCommandValidator(), settings);
        }

        [Test]
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