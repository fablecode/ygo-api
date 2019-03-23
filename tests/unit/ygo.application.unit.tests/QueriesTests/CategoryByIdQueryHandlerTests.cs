using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.CategoryById;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CategoryByIdQueryHandlerTests
    {
        private CategoryByIdQueryHandler _sut;
        private ICategoryService _categoryService;

        [SetUp]
        public void SetUp()
        {
            _categoryService = Substitute.For<ICategoryService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new CategoryProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new CategoryByIdQueryHandler(_categoryService, new CategoryByIdQueryValidator(), mapper);
        }

        [Test]
        public async Task Given_An_Invalid_Query_Should_Return_Null()
        {
            // Arrange
            var query = new CategoryByIdQuery
            {
                Id = -1
            };

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_A_Valid_Query_Should_Execute_CategoryById()
        {
            // Arrange
            _categoryService
                .CategoryById(Arg.Any<int>())
                .Returns(new Category());

            var query = new CategoryByIdQuery{ Id = 4};

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _categoryService.Received(1).CategoryById(Arg.Any<int>());
        }

    }
}