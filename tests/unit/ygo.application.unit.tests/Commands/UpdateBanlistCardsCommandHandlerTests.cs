﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateBanlistCards;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class UpdateBanlistCardsCommandHandlerTests
    {
        private UpdateBanlistCardsCommandHandler _sut;
        private IBanlistCardsRepository _banlistCardsRepository;

        [TestInitialize]
        public void Setup()
        {
            _banlistCardsRepository = Substitute.For<IBanlistCardsRepository>();

            _sut = new UpdateBanlistCardsCommandHandler(_banlistCardsRepository, new UpdateBanlistCardsCommandValidator());
        }

        [TestMethod]
        public async Task Given_An_Invalid_UpdateBanlistCardsCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new UpdateBanlistCardsCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Given_An_Invalid_UpdateMonsterCardCommand_Should_Not_Execute_UpdateCard()
        {
            // Arrange
            _banlistCardsRepository.Update(Arg.Any<long>(), Arg.Any<BanlistCard[]>()).Returns(10);
            var command = new UpdateBanlistCardsCommand();

            // Act
            await _sut.Handle(command);

            // Assert
            await _banlistCardsRepository.DidNotReceive().Update(Arg.Any<long>(), Arg.Any<BanlistCard[]>());
        }
    }
}