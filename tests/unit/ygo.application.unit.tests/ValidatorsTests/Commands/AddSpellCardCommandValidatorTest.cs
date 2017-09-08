using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Commands.AddSpellCard;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestClass]
    public class AddSpellCardCommandValidatorTest
    {
        private AddSpellCardCommandValidator _sut;

        [TestInitialize]
        public void SetUp()
        {
            _sut = new AddSpellCardCommandValidator();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Invalid_SpellCardName_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var command = new AddSpellCardCommand { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_SpellCardName_If_Length_Is_Less_Than_2_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddSpellCardCommand { Name = new string('c', 1) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_SpellCardName_If_Length_Is_Greater_Than_255_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddSpellCardCommand { Name = new string('c', 256) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

    }
}