using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.AllCategories;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AllCategoriesQueryHandlerTests
    {
        private ICategoryService _categoryService;
        private AllCategoriesQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _categoryService = Substitute.For<ICategoryService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new CategoryProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new AllCategoriesQueryHandler(_categoryService, mapper);
        }


        [Test]
        public async Task Given_An_AllCategories_Query_Should_Return_All_Categories()
        {
            // Arrange
            const int expected = 2;

            _categoryService.AllCategories().Returns(new List<Category> { new Category(), new Category() });

            // Act
            var result = await _sut.Handle(new AllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_An_AllCategories_Query_Should_Invoke_AllCategories_Method_Once()
        {
            // Arrange
            _categoryService.AllCategories().Returns(new List<Category> { new Category(), new Category() });

            // Act
            await _sut.Handle(new AllCategoriesQuery(), CancellationToken.None);

            // Assert
            await _categoryService.Received(1).AllCategories();
        }

    }
}