using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.application.Queries.CardById;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    public class CardByIdQueryHandlerTests
    {
        private CardByIdQueryHandler _sut;
        private ICardRepository _cardRepository;

        [SetUp]
        public void SetUp()
        {
            _cardRepository = Substitute.For<ICardRepository>();

            _sut = new CardByIdQueryHandler(_cardRepository, new CardByIdQueryValidator());
        }

        [Test]
        public async Task Given_An_Invalid_Card_Name_Should_Return_Null()
        {
            // Arrange
            var query = new CardByIdQuery();

            // Act
            var result = await _sut.Handle(query);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_An_Valid_Query_Should_Execute_CardById()
        {
            // Arrange
            _cardRepository
                .CardById(Arg.Any<long>())
                .Returns(new Card());

            var query = new CardByIdQuery { Id = 696865 };

            // Act
            await _sut.Handle(query);

            // Assert
            await _cardRepository.Received(1).CardById(Arg.Any<long>());
        }
    }
}