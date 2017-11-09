using System;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    public class LatestBanlistByFormatQueryValidatorTests
    {
        private LatestBanlistQueryValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LatestBanlistQueryValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
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
