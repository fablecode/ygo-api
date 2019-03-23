using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.BanlistServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class BanlistExistTests
    {
        private IBanlistRepository _banlistRepository;
        private BanlistService _sut;

        [SetUp]
        public void SetUp()
        {
            _banlistRepository = Substitute.For<IBanlistRepository>();
            _sut = new BanlistService(_banlistRepository);
        }

        [Test]
        public async Task Given_A_Banlist_Should_Invoke_BanlistExist_Method_Once()
        {
            // Arrange
            const long banlistId = 54353;

            _banlistRepository.BanlistExist(Arg.Any<long>()).Returns(true);

            // Act
            await _sut.BanlistExist(banlistId);

            // Assert
            await _banlistRepository.Received(1).BanlistExist(Arg.Is(banlistId));
        }
    }
}