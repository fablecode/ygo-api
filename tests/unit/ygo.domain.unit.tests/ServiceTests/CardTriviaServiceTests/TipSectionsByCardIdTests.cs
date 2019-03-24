using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardTriviaServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class TipSectionsByCardIdTests
    {
        private ICardTriviaRepository _cardTriviaRepository;
        private CardTriviaService _sut;

        [SetUp]
        public void SetUp()
        {
            _cardTriviaRepository = Substitute.For<ICardTriviaRepository>();
            _sut = new CardTriviaService(_cardTriviaRepository);
        }

        [Test]
        public async Task Given_A_CardId_Should_Invoke_TipSectionsByCardId_Once()
        {
            // Arrange
            const long cardId = 42342;

            // Act
            await _sut.TriviaSectionsByCardId(cardId);

            // Assert
            await _cardTriviaRepository.TriviaSectionsByCardId(Arg.Is(cardId));
        }
    }
}