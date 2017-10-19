using System;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Commands.UpdateBanlistCards;
using ygo.application.Dto;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestClass]
    public class UpdateBanlistCardsCommandValidatorTests
    {
        private UpdateBanlistCardsCommandValidator _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new UpdateBanlistCardsCommandValidator();
        }

        [TestMethod]
        public void Given_A_Null_BanlistCards_Collection_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateBanlistCardsCommand();

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.BanlistCards, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_Empty_BanlistCards_Collection_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateBanlistCardsCommand { BanlistCards = new List<BanlistCardDto>()};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.BanlistCards, command);

            // Assert
            act.Invoke();
        }
    }
}