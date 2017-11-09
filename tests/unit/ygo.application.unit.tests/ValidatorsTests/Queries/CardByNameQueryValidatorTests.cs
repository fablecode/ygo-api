using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ygo.application.Queries.CardByName;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestClass]
    public class CardByNameQueryValidatorTests
    {
        private CardByNameQueryValidator _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new CardByNameQueryValidator();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Invalid_Card_Name_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var query = new CardByNameQuery { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(q => q.Name, query);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_An_Invalid_Card_Name_Validation_Should_Fail()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new CardByNameQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod]
        public void Given_An_Invalid_Card_Name_Validation_Should_StopOnFirstFailure()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new CardByNameQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod]
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