using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateSpellCard;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class UpdateSpellCardCommandHandlerTests
    {
        private UpdateSpellCardCommandHandler _sut;
        private ICardRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICardRepository>();

            _sut = new UpdateSpellCardCommandHandler(_repository, new UpdateSpellCardCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_UpdateSpellCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new UpdateSpellCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateSpellCardCommand_Should_Not_Execute_UpdateCard()
        {
            // Arrange
            _repository.Update(Arg.Any<Card>()).Returns(new Card());
            var command = new UpdateSpellCardCommand();

            // Act
            await _sut.Handle(command);

            // Assert
            _repository.DidNotReceive();
        }

        [Test]
        public async Task Given_An_Valid_UpdateSpellCardCommand_Should_Execute_UpdateCard()
        {
            // Arrange
            _repository.Update(Arg.Any<Card>()).Returns(new Card());
            _repository.CardById(Arg.Any<int>()).Returns(new Card());
            var command = new UpdateSpellCardCommand
            {
                Name = "Monster Reborn",
                SubCategoryIds = new List<int>
                {
                    15 // Normal Spell
                },
                Description = "Target 1 monster in either player's Graveyard; Special Summon it.",
                CardNumber = 83764718
            };

            // Act
            await _sut.Handle(command);

            // Assert
            await _repository.Received(1).Update(Arg.Any<Card>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateSpellCardCommand_ISuccessful_Flag_Should_True()
        {
            // Arrange
            _repository.Update(Arg.Any<Card>()).Returns(new Card());
            _repository.CardById(Arg.Any<int>()).Returns(new Card());
            var command = new UpdateSpellCardCommand
            {
                Name = "Monster Reborn",
                SubCategoryIds = new List<int>
                {
                    15 // Normal Spell
                },
                Description = "Target 1 monster in either player's Graveyard; Special Summon it.",
                CardNumber = 83764718
            };

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}