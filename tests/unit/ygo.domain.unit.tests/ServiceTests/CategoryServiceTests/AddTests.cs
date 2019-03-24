using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CategoryServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddTests
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
        public async Task Given_A_Category_Should_Invoke_Add_Method_Once()
        {
            // Arrange
            var category = new Category();

            // Act
            await _sut.Add(category);
            
            // Assert
            await _categoryRepository.Received(1).Add(Arg.Is(category));
        }
    }
}