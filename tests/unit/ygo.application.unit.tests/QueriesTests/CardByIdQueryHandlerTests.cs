using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Queries.CardById;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestClass]
    public class CardByIdQueryHandlerTests
    {
        private CardByIdQueryHandler _sut;
        private ICardRepository _cardRepository;

        [TestInitialize]
        public void Setup()
        {
            _cardRepository = Substitute.For<ICardRepository>();

            _sut = new CardByIdQueryHandler(_cardRepository, new CardByIdQueryValidator());
        }

        [TestMethod]
        public void Given_An_Invalid_Card_Name_Should_Return_Null()
        {
            // Arrange
            var query = new CardByIdQuery();

            // Act
            var result = _sut.Handle(query);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task Given_An_Valid_Query_Should_Execute_CardByName()
        {
            // Arrange
            _cardRepository
                .CardByName(Arg.Any<string>())
                .Returns(new Card());

            var query = new CardByIdQuery { Id = 696865 };

            // Act
            await _sut.Handle(query);

            // Assert
            await _cardRepository.Received(1).CardByName(Arg.Any<string>());
        }
    }
}