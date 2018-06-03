using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.UpdateTrivias;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class UpdateTriviaCommandValidatorTests
    {
        private UpdateTriviaCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateTriviaCommandValidator();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_CardId_Validation_Should_Fail(long cardId)
        {
            // Arrange
            var command = new UpdateTriviaCommand { CardId = cardId };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardId, command);

            // Assert
            act.Invoke();
        }
    }
}