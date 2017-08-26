using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using ygo.application.Queries.CategoryById;

namespace ygo.application.unit.tests.ValidatorsTests
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
            var result = _sut.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}