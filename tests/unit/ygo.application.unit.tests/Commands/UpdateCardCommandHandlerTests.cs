using System.Threading;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateCard;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class UpdateCardCommandHandlerTests
    {
        private UpdateCardCommandHandler _sut;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
             var settings = Substitute.For<IOptions<ApplicationSettings>>();

            _sut = new UpdateCardCommandHandler(_mediator, new UpdateCardCommandValidator(), settings);
        }

        [Test]
        public async Task Given_An_Invalid_UpdateCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new UpdateCardCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }
    }
}