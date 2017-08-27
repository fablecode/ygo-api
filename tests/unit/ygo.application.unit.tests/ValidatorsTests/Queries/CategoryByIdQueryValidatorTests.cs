using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Queries.CategoryById;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestClass]
    public class CategoryByIdQueryValidatorTests
    {
        private CategoryByIdQueryValidator _sut;

        [TestInitialize]
        public void SetUp()
        {
            _sut = new CategoryByIdQueryValidator();
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void Given_A_Category_Id_LessThan_Or_Equal_To_Zero_Should_Fail_Validation(int categoryId)
        {
            // Arrange
            var query = new CategoryByIdQuery() { Id = categoryId};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(parameter => parameter.Id, query);

            // Assert
            act.Invoke();
        }

        [TestMethod]
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