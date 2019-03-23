using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateTrivia;
using ygo.application.Dto;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateTriviaCommandHandlerTests
    {
        private UpdateTriviaCommandHandler _sut;
        private ICardTriviaService _cardTriviaService;

        [SetUp]
        public void SetUp()
        {
            _cardTriviaService = Substitute.For<ICardTriviaService>();
            _sut = new UpdateTriviaCommandHandler(_cardTriviaService, new UpdateTriviaCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_UpdateTriviaCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateTriviaCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateTriviaCommand_Validation_Should_Return_Error_List()
        {
            // Arrange
            var command = new UpdateTriviaCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Valid_UpdateTriviaCommand_If_TriviaList_Is_Empty_Should_Not_Execute_Update_Method()
        {
            // Arrange
            var command = new UpdateTriviaCommand
            {
                CardId = 34535,
                Trivia = new List<TriviaSectionDto>()
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardTriviaService.DidNotReceive().Update(Arg.Any<List<TriviaSection>>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateTriviaCommand_If_TriviaList_Is_Not_Empty_Should_Execute_Update_Method()
        {
            // Arrange
            var command = new UpdateTriviaCommand
            {
                CardId = 34535,
                Trivia = new List<TriviaSectionDto>
                {
                    new TriviaSectionDto
                    {
                        Name = "List of Monsters",
                        Trivia = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardTriviaService.Received(1).Update(Arg.Any<List<TriviaSection>>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateTriviaCommand_Should_Execute_DeleteByCardId_Method()
        {
            // Arrange
            var command = new UpdateTriviaCommand
            {
                CardId = 34535,
                Trivia = new List<TriviaSectionDto>
                {
                    new TriviaSectionDto
                    {
                        Name = "List of Monsters",
                        Trivia = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardTriviaService.Received(1).DeleteByCardId(Arg.Any<long>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateTriviaCommand_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateTriviaCommand
            {
                CardId = 34535,
                Trivia = new List<TriviaSectionDto>
                {
                    new TriviaSectionDto
                    {
                        Name = "List of Monsters",
                        Trivia = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}