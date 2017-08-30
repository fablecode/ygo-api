using System;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Commands.AddCard;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestClass]
    public class AddCardCommandValidatorTests
    {
        private AddCardCommandValidator _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new AddCardCommandValidator();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Invalid_CardName_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var command = new AddCardCommand{ Name = cardName};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }
    }

    public class AddCardCommandValidator : AbstractValidator<AddCardCommand>
    {
        public AddCardCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Length(2, 255);
        }
    }
}