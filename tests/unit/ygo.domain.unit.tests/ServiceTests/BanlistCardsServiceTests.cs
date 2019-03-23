using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class BanlistCardsServiceTests
    {
        private IBanlistCardsRepository _banlistCardsRepository;
        private BanlistCardsService _sut;

        [SetUp]
        public void SetUp()
        {
            _banlistCardsRepository = Substitute.For<IBanlistCardsRepository>();
            _sut = new BanlistCardsService(_banlistCardsRepository);
        }

        [Test]
        public async Task Given_An_BanlistId_And_Cards_Should_Invoke_Update_Method_Once()
        {
            // Arrange
            const int banlistId = 423;
            var cards = new BanlistCard[0];

            _banlistCardsRepository.Update(Arg.Any<long>(), Arg.Any< BanlistCard[]> ()).Returns(new List<BanlistCard>());

            // Act
            await _sut.Update(banlistId, cards);

            // Assert
            await _banlistCardsRepository.Received(1).Update(Arg.Is<long>(banlistId), Arg.Is(cards));
        }
    }
}