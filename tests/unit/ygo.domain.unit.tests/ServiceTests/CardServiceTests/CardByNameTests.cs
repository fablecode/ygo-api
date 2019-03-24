using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.core.Strategies;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardByNameTests
    {
        private IEnumerable<ICardTypeStrategy> _cardTypeStrategies;
        private ICardRepository _cardRepository;
        private CardService _sut;

        [SetUp]
        public void SetUp()
        {
            _cardTypeStrategies = Substitute.For<IEnumerable<ICardTypeStrategy>>();
            _cardRepository = Substitute.For<ICardRepository>();
            _sut = new CardService(_cardTypeStrategies, _cardRepository);
        }

        [Test]
        public async Task Given_A_Card_Name_Should_Invoke_CardByName_Method_Once()
        {
            // Arrange
            const string cardName = "Blue-Eyes White Dragon";

            _cardRepository.CardByName(Arg.Any<string>()).Returns(new Card());

            // Act
            await _sut.CardByName(cardName);

            // Assert
            await _cardRepository.Received(1).CardByName(Arg.Is(cardName));
        }
    }
}