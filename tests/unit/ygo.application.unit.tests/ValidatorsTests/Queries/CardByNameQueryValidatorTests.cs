﻿using System;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ygo.application.Queries.CardByName;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    public class CardByNameQueryValidatorTests
    {
        private CardByNameQueryValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardByNameQueryValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_Card_Name_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var query = new CardByNameQuery { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(q => q.Name, query);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_An_Invalid_Card_Name_Validation_Should_Fail()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new CardByNameQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [Test]
        public void Given_An_Invalid_Card_Name_Validation_Should_StopOnFirstFailure()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new CardByNameQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [Test]
        public void Given_An_Valid_Card_Name_Validation_Should_Pass()
        {
            // Arrange
            var query = new CardByNameQuery { Name = "GoodCardName"};

            // Act
            var result = _sut.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}