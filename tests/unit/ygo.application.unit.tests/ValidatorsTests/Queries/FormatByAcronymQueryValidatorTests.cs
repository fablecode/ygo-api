using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Queries.FormatByAcronym;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestClass]
    public class FormatByAcronymQueryValidatorTests
    {
        private FormatByAcronymQueryValidator _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new FormatByAcronymQueryValidator();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("uyhh")]
        [DataRow("uy")]
        [DataRow("y")]
        public void Given_An_Invalid_Acronym_Validation_Should_Fail(string acronym)
        {
            // Arrange
            var query = new FormatByAcronymQuery { Acronym = acronym };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(q => q.Acronym, query);

            // Assert
            act.Invoke();
        }

        [DataTestMethod]
        [DataRow("tcg")]
        [DataRow("ocg")]
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