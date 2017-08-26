using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Queries.CategoryById;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    public class CategoryByIdQueryHandlerTests
    {
        private CategoryByIdQueryHandler _sut;
        private ICategoryRepository _categoryRepository;

        [SetUp]
        public void Setup()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();

            _sut = new CategoryByIdQueryHandler(_categoryRepository, new CategoryByIdQueryValidator());
        }

        [Test]
        public void Given_An_Invalid_Query_Should_Throw_ValidationException()
        {
            // Arrange
            var query = new CategoryByIdQuery();

            // Act
            Action act = () => _sut.Handle(query);

            // Assert
            act.ShouldThrow<ValidationException>();
        }

        [Test]
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