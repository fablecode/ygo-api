using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Commands.AddCard;
using ygo.core.Enums;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestClass]
    public class AddCardCommandValidatorTests
    {
        private AddCardCommandValidator _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new AddCardCommandValidator();
        }


        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        public void Given_An_Invalid_CardType_Validation_Should_Fail(YgoCardType cardType)
        {
            // Arrange
            var command = new AddCardCommand{ CardType = cardType};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardType, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_Null_CardType_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddCardCommand();

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardType, command);

            // Assert
            act.Invoke();
        }


    }
}