using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class TypeServiceTests
    {
        private ITypeRepository _typeRepository;
        private TypeService _sut;

        [SetUp]
        public void SetUp()
        {
            _typeRepository = Substitute.For<ITypeRepository>();
            _sut = new TypeService(_typeRepository);
        }

        [Test]
        public async Task Should_Invoke_AllTypes_Method_Once()
        {
            // Arrange
            // Act
            await _sut.AllTypes();

            // Assert
            await _typeRepository.Received(1).AllTypes();
        }
    }
}