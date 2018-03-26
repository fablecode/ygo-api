using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.AddCard;
using ygo.core.Enums;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class AddCardCommandValidatorTests
    {
        private AddCardCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AddCardCommandValidator();
        }


        [TestCase(-1)]
        [TestCase(3)]
        public void Given_An_Invalid_CardType_Validation_Should_Fail(YgoCardType cardType)
        {
            // Arrange
            var command = new AddCardCommand{ CardType = cardType};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardType, command);

            // Assert
            act.Invoke();
        }

        [Test]
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