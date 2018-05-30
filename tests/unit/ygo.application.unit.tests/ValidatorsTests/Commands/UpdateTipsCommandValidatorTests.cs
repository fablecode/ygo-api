﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.UpdateTips;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class UpdateTipsCommandValidatorTests
    {
        private UpdateTipsCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateTipsCommandValidator();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(" ")]
        public void Given_An_Invalid_CardId_Validation_Should_Fail(long cardId)
        {
            // Arrange
            var command = new UpdateTipsCommand { CardId = cardId };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardId, command);

            // Assert
            act.Invoke();
        }
    }
}