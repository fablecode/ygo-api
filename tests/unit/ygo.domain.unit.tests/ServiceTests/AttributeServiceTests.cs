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
    public class AttributeServiceTests
    {
        private IAttributeRepository _attributeRepository;
        private AttributeService _sut;

        [SetUp]
        public void SetUp()
        {
            _attributeRepository = Substitute.For<IAttributeRepository>();

            _sut = new AttributeService(_attributeRepository);
        }

        [Test]
        public async Task Should_Invoke_AllAttributes_Method_Once()
        {
            // Arrange
            // Act
            await _sut.AllAttributes();

            // Assert
            await _attributeRepository.Received(1).AllAttributes();
        }
    }
}