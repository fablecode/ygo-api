using System;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestClass]
    public class LatestBanlistByFormatQueryValidatorTests
    {
        private LatestBanlistQueryValidator _sut;

        [TestInitialize]
        public void Setup()
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
            var query = new LatestBanlistQuery { Format = format };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(lbl => lbl.Format, query);

            // Assert
            act.Invoke();
        }

    }

    public class LatestBanlistQueryValidator : AbstractValidator<LatestBanlistQuery>
    {
        public LatestBanlistQueryValidator()
        {
            RuleFor(lb => lb.Format)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
        }
    }

    public class LatestBanlistQuery
    {
        public string Format { get; set; }
    }
}
