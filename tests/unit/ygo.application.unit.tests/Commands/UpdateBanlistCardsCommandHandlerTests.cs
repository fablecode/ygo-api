using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateBanlistCards;
using ygo.application.Dto;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateBanlistCardsCommandHandlerTests
    {
        private UpdateBanlistCardsCommandHandler _sut;
        private IBanlistCardsService _banlistCardsService;

        [SetUp]
        public void SetUp()
        {
            _banlistCardsService = Substitute.For<IBanlistCardsService>();

            _sut = new UpdateBanlistCardsCommandHandler(_banlistCardsService, new UpdateBanlistCardsCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_UpdateBanlistCardsCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateBanlistCardsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }
        [Test]
        public async Task Given_An_Invalid_UpdateBanlistCardsCommand_Validation_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new UpdateBanlistCardsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }


        [Test]
        public async Task Given_An_Invalid_UpdateMonsterCardCommand_Should_Not_Execute_UpdateCard()
        {
            // Arrange
            _banlistCardsService.Update(Arg.Any<long>(), Arg.Any<BanlistCard[]>()).Returns(new List<BanlistCard>());
            var command = new UpdateBanlistCardsCommand();

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistCardsService.DidNotReceive().Update(Arg.Any<long>(), Arg.Any<BanlistCard[]>());
        }

        [Test]
        public async Task Given_An_Valid_BanlistCards_Should_Execute_Command_Successfully()
        {
            // Arrange
            var command = new UpdateBanlistCardsCommand
            {
                BanlistId = 22342,
                BanlistCards = new List<BanlistCardDto>
                {
                    new BanlistCardDto{ BanlistId = 234234, CardId = 23424 }
                }
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_An_Valid_BanlistCards_Should_Execute_Update_Method_Once()
        {
            // Arrange
            var command = new UpdateBanlistCardsCommand
            {
                BanlistId = 22342,
                BanlistCards = new List<BanlistCardDto>
                {
                    new BanlistCardDto{ BanlistId = 234234, CardId = 23424 }
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistCardsService.Received(1).Update(Arg.Any<long>(), Arg.Any<BanlistCard[]>());
        }
    }
}