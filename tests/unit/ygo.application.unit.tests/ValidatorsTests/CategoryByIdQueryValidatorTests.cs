using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Queries.CategoryById;

namespace ygo.application.unit.tests.ValidatorsTests
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
            var result = _sut.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}