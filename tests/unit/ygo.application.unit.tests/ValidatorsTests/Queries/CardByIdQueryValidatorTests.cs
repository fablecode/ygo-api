using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Queries.CardById;
using ygo.tests.core;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardByIdQueryValidatorTests
    {
        private CardByIdQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CardByIdQueryValidator();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_Card_Id_Validation_Should_Fail(long cardId)
        {
            // Arrange
            var query = new CardByIdQuery { Id = cardId };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(q => q.Id, query);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_An_Invalid_Card_Id_Validation_Should_Fail()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new CardByIdQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [Test]
        public void Given_An_Invalid_Card_Id_Validation_Should_StopOnFirstFailure()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new CardByIdQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [Test]
        public void Given_An_Valid_Card_Id_Validation_Should_Pass()
        {
            // Arrange
            var query = new CardByIdQuery { Id = 582941};

            // Act
            var result = _sut.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}