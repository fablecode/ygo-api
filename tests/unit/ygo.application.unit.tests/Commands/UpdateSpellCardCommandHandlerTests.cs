using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands.UpdateSpellCard;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class UpdateSpellCardCommandHandlerTests
    {
        private UpdateSpellCardCommandHandler _sut;
        private ICardRepository _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<ICardRepository>();

            _sut = new UpdateSpellCardCommandHandler(_repository, new UpdateSpellCardCommandValidator());
        }

        [TestMethod]
        public async Task Given_An_Invalid_UpdateSpellCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new UpdateSpellCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [TestMethod]
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

        [TestMethod]
        public async Task Given_An_Valid_UpdateSpellCardCommand_Should_Execute_UpdateCard()
        {
            // Arrange
            _repository.Update(Arg.Any<Card>()).Returns(new Card());
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

        [TestMethod]
        public async Task Given_An_Valid_UpdateSpellCardCommand_ISuccessful_Flag_Should_True()
        {
            // Arrange
            _repository.Update(Arg.Any<Card>()).Returns(new Card());
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