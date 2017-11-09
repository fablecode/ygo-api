using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ygo.application.Queries.FormatByAcronym;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    public class FormatByAcronymQueryValidatorTests
    {
        private FormatByAcronymQueryValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new FormatByAcronymQueryValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("uyhh")]
        [TestCase("uy")]
        [TestCase("y")]
        public void Given_An_Invalid_Acronym_Validation_Should_Fail(string acronym)
        {
            // Arrange
            var query = new FormatByAcronymQuery { Acronym = acronym };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(q => q.Acronym, query);

            // Assert
            act.Invoke();
        }

        [TestCase("tcg")]
        [TestCase("ocg")]
        public void Given_A_Valid_Acronym_Validation_Should_Pass(string acronym)
        {
            // Arrange
            var query = new FormatByAcronymQuery { Acronym = acronym };

            // Act
            Action act = () => _sut.ShouldNotHaveValidationErrorFor(q => q.Acronym, query);

            // Assert
            act.Invoke();
        }

    }
}