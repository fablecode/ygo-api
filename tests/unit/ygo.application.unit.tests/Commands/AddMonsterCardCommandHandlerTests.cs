using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo.application.Commands.AddMonsterCard;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.infrastructure.Database;
using ygo.infrastructure.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class AddMonsterCardCommandHandlerTests
    {
        private AddMonsterCardCommandHandler _sut;
        private ICardRepository _repository;
        private YgoDbContext _testContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<YgoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _testContext = new YgoDbContext(options);

            _repository = Substitute.For<ICardRepository>();

            _sut = new AddMonsterCardCommandHandler(_repository, new AddMonsterCardCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_AddMonsterCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = new AddMonsterCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_AddMonsterCardCommand_Should_Not_Execute_AddCard()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = new AddMonsterCardCommand();

            // Act
            await _sut.Handle(command);

            // Assert
            await _repository.DidNotReceive().Update(Arg.Any<Card>());
        }

        [Test]
        public async Task Given_A_Valid_AddMonsterCardCommand_Should_Execute_AddCard()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = GetValidMonsterCard();

            // Act
            await _sut.Handle(command);

            // Assert
            await _repository.Received(1).Add(Arg.Any<Card>());
        }

        [Test]
        public async Task Given_A_Valid_AddMonsterCardCommand_ISuccessful_Flag_Should_True()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = GetValidMonsterCard();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_A_Valid_AddMonsterCardCommand_Should_Save_Card_To_Database()
        {
            // Arrange
            _repository = new CardRepository(_testContext);
            _sut = new AddMonsterCardCommandHandler(_repository, new AddMonsterCardCommandValidator());

            var command = GetValidMonsterCard();

            // Act
            var result = await _sut.Handle(command);
            var insertedCardId = (long)result.Data;

            // Assert
            _testContext.Card.Count(c => c.Id == insertedCardId).Should().Be(1);
        }


        private static AddMonsterCardCommand GetValidMonsterCard()
        {
            return new AddMonsterCardCommand
            {
                Name = "Blue-Eyes White Dragon",
                AttributeId = 5, // Light,
                CardLevel = 8,
                SubCategoryIds = new List<int>
                {
                    1 // Normal Monster
                },
                TypeIds = new List<int>
                {
                    8 // Dragon
                },
                Description = "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.",
                Atk = 3000,
                Def = 2500,
                CardNumber = 89631139,
            };
        }
    }
}