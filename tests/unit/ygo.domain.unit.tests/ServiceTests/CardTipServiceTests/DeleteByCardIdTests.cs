using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardTipServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class DeleteByCardIdTests
    {
        private ICardTipRepository _cardTipRepository;
        private CardTipService _sut;

        [SetUp]
        public void SetUp()
        {
            _cardTipRepository = Substitute.For<ICardTipRepository>();
            _sut = new CardTipService(_cardTipRepository);
        }

        [Test]
        public async Task Given_A_CardId_Should_Invoke_DeleteByCardId_Once()
        {
            // Arrange
            const long cardId = 42342;

            // Act
            await _sut.DeleteByCardId(cardId);

            // Assert
            await _cardTipRepository.DeleteByCardId(Arg.Is(cardId));
        }
    }
}