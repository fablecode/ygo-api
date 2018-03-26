using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Queries.LatestBanlistByFormat;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    public class LatestBanlistQueryValidatorTests
    {
        private LatestBanlistQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LatestBanlistQueryValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
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
