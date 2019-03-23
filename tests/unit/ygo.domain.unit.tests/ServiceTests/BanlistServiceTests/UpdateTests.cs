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
    public class UpdateTests
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
        public async Task Given_A_Banlist_Should_Invoke_Update_Method_Once()
        {
            // Arrange
            var banlist = new Banlist();

            _banlistRepository.Update(Arg.Any<Banlist>()).Returns(new Banlist());

            // Act
            await _sut.Update(banlist);

            // Assert
            await _banlistRepository.Received(1).Update(Arg.Is(banlist));
        }
    }
}