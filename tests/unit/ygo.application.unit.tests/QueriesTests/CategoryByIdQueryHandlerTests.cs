using System.Threading;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.application.Queries.CategoryById;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CategoryByIdQueryHandlerTests
    {
        private CategoryByIdQueryHandler _sut;
        private ICategoryRepository _categoryRepository;

        [SetUp]
        public void SetUp()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();

            _sut = new CategoryByIdQueryHandler(_categoryRepository, new CategoryByIdQueryValidator());
        }

        [Test]
        public void Given_An_Invalid_Query_Should_Return_Null()
        {
            // Arrange
            var query = new CategoryByIdQuery();

            // Act
            var result = _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_A_Valid_Query_Should_Execute_CategoryById()
        {
            // Arrange
            _categoryRepository
                .CategoryById(Arg.Any<int>())
                .Returns(new Category());

            var query = new CategoryByIdQuery{ Id = 4};

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _categoryRepository.Received(1).CategoryById(Arg.Any<int>());
        }

    }
}