using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardTriviaServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateTests
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
        public async Task Given_TipSections_Should_Invoke_Update_Once()
        {
            // Arrange
            var triviaSections = new List<TriviaSection>();

            // Act
            await _sut.Update(triviaSections);

            // Assert
            await _cardTriviaRepository.Update(Arg.Is(triviaSections));
        }
    }
}