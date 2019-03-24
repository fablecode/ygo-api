using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CategoryServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AllCategoriesTests
    {
        private ICategoryRepository _categoryRepository;
        private CategoryService _sut;

        [SetUp]
        public void SetUp()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();
            _sut = new CategoryService(_categoryRepository);
        }

        [Test]
        public async Task Should_Invoke_AllCategories_Method_Once()
        {
            // Arrange
            // Act
            await _sut.AllCategories();

            // Assert
            await _categoryRepository.Received(1).AllCategories();
        }
    }
}