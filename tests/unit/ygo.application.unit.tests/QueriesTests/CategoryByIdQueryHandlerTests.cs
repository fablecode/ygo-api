using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Queries.CategoryById;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestClass]
    public class CategoryByIdQueryHandlerTests
    {
        private CategoryByIdQueryHandler _sut;
        private ICategoryRepository _categoryRepository;

        [TestInitialize]
        public void Setup()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();

            _sut = new CategoryByIdQueryHandler(_categoryRepository, new CategoryByIdQueryValidator());
        }

        [TestMethod]
        public void Given_An_Invalid_Query_Should_Return_Null()
        {
            // Arrange
            var query = new CategoryByIdQuery();

            // Act
            var result = _sut.Handle(query);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task Given_A_Valid_Query_Should_Execute_GetCategoryById()
        {
            // Arrange
            _categoryRepository
                .GetCategoryById(Arg.Any<int>())
                .Returns(new Category());

            var query = new CategoryByIdQuery{ Id = 4};

            // Act
            await _sut.Handle(query);

            // Assert
            await _categoryRepository.Received(1).GetCategoryById(Arg.Any<int>());
        }

    }
}