using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateBanlistCards;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class UpdateBanlistCardsCommandHandlerTests
    {
        private UpdateBanlistCardsCommandHandler _sut;
        private IBanlistCardsRepository _banlistCardsRepository;

        [SetUp]
        public void SetUp()
        {
            _banlistCardsRepository = Substitute.For<IBanlistCardsRepository>();

            _sut = new UpdateBanlistCardsCommandHandler(_banlistCardsRepository, new UpdateBanlistCardsCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_UpdateBanlistCardsCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
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
            _banlistCardsRepository.Update(Arg.Any<long>(), Arg.Any<BanlistCard[]>()).Returns(new List<BanlistCard>());
            var command = new UpdateBanlistCardsCommand();

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistCardsRepository.DidNotReceive().Update(Arg.Any<long>(), Arg.Any<BanlistCard[]>());
        }
    }
}