using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands.AddSpellCard;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class AddSpellCardCommandHandlerTests
    {
        private AddSpellCardCommandHandler _sut;
        private ICardRepository _repository;
        private YgoDbContext _testContext;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<YgoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _testContext = new YgoDbContext(options);

            _repository = Substitute.For<ICardRepository>();

            _sut = new AddSpellCardCommandHandler(_repository, new AddSpellCardCommandValidator());
        }

        [TestMethod]
        public async Task Given_An_Invalid_AddSpellCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new AddSpellCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Given_An_Invalid_AddSpellCardCommand_Should_Not_Execute_AddCard()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = new AddSpellCardCommand();

            // Act
            await _sut.Handle(command);

            // Assert
            _repository.DidNotReceive();
        }

        [TestMethod]
        public async Task Given_An_Valid_AddSpellCardCommand_Should_Execute_AddCard()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = GetValidSpellCard();

            // Act
            await _sut.Handle(command);

            // Assert
            await _repository.Received(1).Add(Arg.Any<Card>());
        }

        [TestMethod]
        public async Task Given_An_Valid_AddSpellCardCommand_ISuccessful_Flag_Should_True()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = GetValidSpellCard();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        public async Task Given_A_Valid_AddSpellCardCommand_Should_Save_Card_To_Database()
        {
            // Arrange
            _repository = new CardRepository(_testContext);
            _sut = new AddSpellCardCommandHandler(_repository, new AddSpellCardCommandValidator());

            var command = GetValidSpellCard();

            // Act
            var result = await _sut.Handle(command);
            var insertedCardId = (long)result.Data;

            // Assert
            _testContext.Card.Count(c => c.Id == insertedCardId).Should().Be(1);
        }

        private static AddSpellCardCommand GetValidSpellCard()
        {
            return new AddSpellCardCommand
            {
                Name = "Monster Reborn",
                SubCategoryIds = new List<int>
                {
                    15 // Normal Spell
                },
                Description = "Target 1 monster in either player's Graveyard; Special Summon it.",
                CardNumber = 83764718
            };
        }


    }
}