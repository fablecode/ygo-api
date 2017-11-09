using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.AddCategory;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class AddCategoryCommandValidatorTests
    {
        private AddCategoryCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new AddCategoryCommandValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_A_Invalid_Category_Name_Validation_Should_Fail(string categoryName)
        {
            // Arrange
            var command = new AddCategoryCommand{ Name = categoryName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(category => category.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_A_Category_Name_Whose_Length_Is_Not_Between_3_And_255_Validation_Should_Fail()
        {
            // Arrange
            var name = new string('c', 256);
            var command = new AddCategoryCommand{ Name = name };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(category => category.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_A_Valid_Category_Name_Validation_Should_Pass()
        {
            // Arrange
            var name = "category";
            var command = new AddCategoryCommand { Name = name };

            // Act
            Action act = () => _sut.ShouldNotHaveValidationErrorFor(category => category.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_A_Invalid_Category_Name_Validation_Should_StopOnFirstFailure()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new AddCategoryCommand());

            // Assert
            result.Errors.Should().HaveCount(1);
        }
    }
}