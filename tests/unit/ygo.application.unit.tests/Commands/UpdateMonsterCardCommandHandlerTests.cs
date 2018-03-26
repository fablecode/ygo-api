using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateMonsterCard;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class UpdateMonsterCardCommandHandlerTests
    {
        private UpdateMonsterCardCommandHandler _sut;
        private ICardRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICardRepository>();

            _sut = new UpdateMonsterCardCommandHandler(_repository, new UpdateMonsterCardCommandValidator());

        }

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Configure();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateMonsterCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new UpdateMonsterCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateMonsterCardCommand_Should_Not_Execute_UpdateCard()
        {
            // Arrange
            _repository.Update(Arg.Any<Card>()).Returns(new Card());
            var command = new UpdateMonsterCardCommand();

            // Act
            await _sut.Handle(command);

            // Assert
            await _repository.DidNotReceive().Update(Arg.Any<Card>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateMonsterCardCommand_Should_Execute_UpdateCard()
        {
            // Arrange
            _repository.CardById(Arg.Any<long>()).Returns(new Card());
            _repository.Update(Arg.Any<Card>()).Returns(new Card());

            var command = GetValidUpdateMonsterCardCommand();

            // Act
            await _sut.Handle(command);

            // Assert
            await _repository.Received(1).Update(Arg.Any<Card>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateMonsterCardCommand_ISuccessful_Flag_Should_True()
        {
            // Arrange
            _repository.CardById(Arg.Any<long>()).Returns(new Card());
            _repository.Update(Arg.Any<Card>()).Returns(new Card());
            var command = GetValidUpdateMonsterCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        private static UpdateMonsterCardCommand GetValidUpdateMonsterCardCommand()
        {
            return new UpdateMonsterCardCommand
            {
                Id = 534864,
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