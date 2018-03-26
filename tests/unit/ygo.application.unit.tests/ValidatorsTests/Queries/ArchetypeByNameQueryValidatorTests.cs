using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Queries.ArchetypeByName;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    public class ArchetypeByNameQueryValidatorTests
    {
        private ArchetypeByNameQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ArchetypeByNameQueryValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_Archetype_Name_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var query = new ArchetypeByNameQuery { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(q => q.Name, query);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_An_Invalid_Archetype_Name_Validation_Should_StopOnFirstFailure()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new ArchetypeByNameQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [Test]
        public void Given_An_Valid_Card_Name_Validation_Should_Pass()
        {
            // Arrange
            var query = new ArchetypeByNameQuery { Name = "GoodArchetypeName" };

            // Act
            var result = _sut.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}