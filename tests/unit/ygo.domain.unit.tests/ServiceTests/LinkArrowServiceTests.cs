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
    public class LinkArrowServiceTests
    {
        private ILinkArrowRepository _linkArrowRepository;
        private LinkArrowService _sut;

        [SetUp]
        public void SetUp()
        {
            _linkArrowRepository = Substitute.For<ILinkArrowRepository>();
            _sut = new LinkArrowService(_linkArrowRepository);
        }

        [Test]
        public async Task Should_Invoke_AllLinkArrows_Method_Once()
        {
            // Arrange
            // Act
            await _sut.AllLinkArrows();

            // Assert
            await _linkArrowRepository.Received(1).AllLinkArrows();
        }
    }
}