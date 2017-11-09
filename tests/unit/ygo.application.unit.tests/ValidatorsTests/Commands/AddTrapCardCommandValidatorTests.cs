using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.AddTrapCard;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class AddTrapCardCommandValidatorTests
    {
        private AddTrapCardCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AddTrapCardCommandValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_TrapCardName_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var command = new AddTrapCardCommand { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_A_TrapCardName_If_Length_Is_Less_Than_2_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddTrapCardCommand { Name = new string('c', 1) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_A_TrapCardName_If_Length_Is_Greater_Than_255_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddTrapCardCommand { Name = new string('c', 256) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

    }
}