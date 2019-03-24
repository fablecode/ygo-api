using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Enums;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Strategies;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateTests
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
        public async Task Given_A_Card_Should_Invoke_Update_Method_Once()
        {
            // Arrange
            var cardModel = new CardModel
            {
                CardType = YugiohCardType.Monster
            };

            var handler = Substitute.For<ICardTypeStrategy>();
            handler.Handles(Arg.Any<YugiohCardType>()).Returns(true);
            handler.Update(Arg.Any<CardModel>()).Returns(new Card());

            _cardTypeStrategies.GetEnumerator().Returns(new List<ICardTypeStrategy>{ handler }.GetEnumerator());

            // Act
            await _sut.Update(cardModel);

            // Assert
            await handler.Received(1).Update(Arg.Is(cardModel));
        }
    }
}