using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.AllSubCategories;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AllSubCategoriesQueryHandlerTests
    {
        private ISubCategoryService _subCategoryService;
        private AllSubCategoriesQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _subCategoryService = Substitute.For<ISubCategoryService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new SubCategoryProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new AllSubCategoriesQueryHandler(_subCategoryService, mapper);
        }


        [Test]
        public async Task Given_An_AllSubCategories_Query_Should_Return_All_SubCategories()
        {
            // Arrange
            const int expected = 2;

            _subCategoryService.AllSubCategories().Returns(new List<SubCategory> { new SubCategory(), new SubCategory() });

            // Act
            var result = await _sut.Handle(new AllSubCategoriesQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_An_AllSubCategories_Query_Should_Invoke_AllSubCategories_Method_Once()
        {
            // Arrange
            _subCategoryService.AllSubCategories().Returns(new List<SubCategory> { new SubCategory(), new SubCategory() });

            // Act
            await _sut.Handle(new AllSubCategoriesQuery(), CancellationToken.None);

            // Assert
            await _subCategoryService.Received(1).AllSubCategories();
        }

    }
}