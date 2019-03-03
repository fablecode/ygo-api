using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.AddSpellCard;
using ygo.tests.core;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddSpellCardCommandValidatorTest
    {
        private AddSpellCardCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AddSpellCardCommandValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_SpellCardName_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var command = new AddSpellCardCommand { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
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