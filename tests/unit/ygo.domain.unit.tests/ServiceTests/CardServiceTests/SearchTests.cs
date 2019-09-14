using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
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
    public class SearchTests
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
        public async Task Given_A_Card_Id_Should_Invoke_Search_Method_Once()
        {
            // Arrange
            const string searchTerm = "query";
            const int pageIndex = 1;
            const int pageSize = 10;

            _cardRepository.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Card>());

            // Act
            await _sut.Search(searchTerm, pageIndex, pageSize);

            // Assert
            await _cardRepository.Received(1).Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());
        }
    }
}