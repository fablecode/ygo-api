using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Strategies;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardExistsTests
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
        public async Task Given_A_Card_Id_Should_Invoke_CardExists_Method_Once()
        {
            // Arrange
            const long cardId = 4322;

            _cardRepository.CardExists(Arg.Any<long>()).Returns(true);

            // Act
            await _sut.CardExists(cardId);

            // Assert
            await _cardRepository.Received(1).CardExists(Arg.Is(cardId));
        }
    }
}