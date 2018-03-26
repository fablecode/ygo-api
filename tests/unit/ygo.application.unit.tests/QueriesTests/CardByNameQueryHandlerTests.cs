using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.application.Queries.CardByName;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    public class CardByNameQueryHandlerTests
    {
        private CardByNameQueryHandler _sut;
        private ICardRepository _cardRepository;

        [SetUp]
        public void SetUp()
        {
            _cardRepository = Substitute.For<ICardRepository>();

            _sut = new CardByNameQueryHandler(_cardRepository, new CardByNameQueryValidator());
        }

        [Test]
        public async Task Given_An_Invalid_Card_Name_Should_Return_Null()
        {
            // Arrange
            var query = new CardByNameQuery();

            // Act
            var result = await _sut.Handle(query);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_An_Valid_Query_Should_Execute_CardByName()
        {
            // Arrange
            _cardRepository
                .CardByName(Arg.Any<string>())
                .Returns(new Card());

            var query = new CardByNameQuery{ Name = "goodcardname"};

            // Act
            await _sut.Handle(query);

            // Assert
            await _cardRepository.Received(1).CardByName(Arg.Any<string>());
        }

    }
}