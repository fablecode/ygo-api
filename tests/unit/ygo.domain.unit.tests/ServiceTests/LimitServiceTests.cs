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
    public class LimitServiceTests
    {
        private ILimitRepository _limitRepository;
        private LimitService _sut;

        [SetUp]
        public void SetUp()
        {
            _limitRepository = Substitute.For<ILimitRepository>();
            _sut = new LimitService(_limitRepository);
        }

        [Test]
        public async Task Should_Invoke_AllLimits_Method_Once()
        {
            // Arrange
            // Act
            await _sut.AllLimits();

            // Assert
            await _limitRepository.Received(1).AllLimits();
        }
    }
}