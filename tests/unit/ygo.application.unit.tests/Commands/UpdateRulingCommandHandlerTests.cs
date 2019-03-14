using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateRulings;
using ygo.application.Dto;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateRulingCommandHandlerTests
    {
        private UpdateRulingCommandHandler _sut;
        private ICardRulingService _cardRulingService;

        [SetUp]
        public void SetUp()
        {
            _cardRulingService = Substitute.For<ICardRulingService>();
            _sut = new UpdateRulingCommandHandler(_cardRulingService, new UpdateRulingCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_UpdateRulingCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateRulingCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateRulingCommand_Validation_Should_Return_Error_List()
        {
            // Arrange
            var command = new UpdateRulingCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Valid_UpdateRulingCommand_If_RulingsList_Is_Empty_Should_Not_Execute_Update_Method()
        {
            // Arrange
            var command = new UpdateRulingCommand
            {
                CardId = 34535,
                Rulings = new List<RulingSectionDto>()
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardRulingService.DidNotReceive().Update(Arg.Any<List<RulingSection>>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateRulingCommand_If_RulingsList_Is_Not_Empty_Should_Execute_Update_Method()
        {
            // Arrange
            var command = new UpdateRulingCommand
            {
                CardId = 34535,
                Rulings = new List<RulingSectionDto>
                {
                    new RulingSectionDto
                    {
                        Name = "List of Monsters",
                        Rulings = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardRulingService.Received(1).Update(Arg.Any<List<RulingSection>>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateRulingCommand_Should_Execute_DeleteByCardId_Method()
        {
            // Arrange
            var command = new UpdateRulingCommand
            {
                CardId = 34535,
                Rulings = new List<RulingSectionDto>
                {
                    new RulingSectionDto
                    {
                        Name = "List of Monsters",
                        Rulings = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardRulingService.Received(1).DeleteByCardId(Arg.Any<long>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateRulingCommand_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateRulingCommand
            {
                CardId = 34535,
                Rulings = new List<RulingSectionDto>
                {
                    new RulingSectionDto
                    {
                        Name = "List of Monsters",
                        Rulings = new List<string>{ "Sangan" }
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