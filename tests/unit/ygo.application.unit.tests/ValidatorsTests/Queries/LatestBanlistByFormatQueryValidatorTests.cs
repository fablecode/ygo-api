using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentValidation.TestHelper;
using ygo.application.Queries.LatestBanlistByFormat;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestClass]
    public class LatestBanlistByFormatQueryValidatorTests
    {
        private LatestBanlistByFormatQueryValidator _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new LatestBanlistByFormatQueryValidator();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Format_Acronym_Validation_Should_Fail(string format)
        {
            // Arrange
            var query = new LatestBanlistByFormatQuery { Format = format };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(lbl => lbl.Format, query);

            // Assert
            act.Invoke();
        }
    }
}
