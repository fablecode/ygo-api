using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentValidation.TestHelper;
using ygo.application.Queries.LatestBanlistByFormat;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestClass]
    public class LatestBanlistQueryValidatorTests
    {
        private LatestBanlistQueryValidator _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new LatestBanlistQueryValidator();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Format_Acronym_Validation_Should_Fail(string format)
        {
            // Arrange
            var query = new LatestBanlistQuery { Acronym = format };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(lbl => lbl.Acronym, query);

            // Assert
            act.Invoke();
        }
    }
}
