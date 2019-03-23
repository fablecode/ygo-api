using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.BanlistServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetBanlistByIdTests
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
        public async Task Given_A_BanlistId_Should_Invoke_GetBanlistById_Method_Once()
        {
            // Arrange
            const long banlistId = 42423;

            _banlistRepository.GetBanlistById(Arg.Any<long>()).Returns(new Banlist());

            // Act
            await _sut.GetBanlistById(banlistId);

            // Assert
            await _banlistRepository.Received(1).GetBanlistById(Arg.Is(banlistId));
        }

    }
}