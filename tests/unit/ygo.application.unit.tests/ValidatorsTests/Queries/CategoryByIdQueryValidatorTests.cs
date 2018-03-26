using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Queries.CategoryById;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    public class CategoryByIdQueryValidatorTests
    {
        private CategoryByIdQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CategoryByIdQueryValidator();
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Given_A_Category_Id_LessThan_Or_Equal_To_Zero_Should_Fail_Validation(int categoryId)
        {
            // Arrange
            var query = new CategoryByIdQuery() { Id = categoryId};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(parameter => parameter.Id, query);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_A_Valid_Category_Id_Validation_Should_Pass()
        {
            // Arrange
            var query = new CategoryByIdQuery() { Id = 232 };

            // Act
            Action act = () => _sut.ShouldNotHaveValidationErrorFor(parameter => parameter.Id, query);

            // Assert
            act.Invoke();
        }

    }
}