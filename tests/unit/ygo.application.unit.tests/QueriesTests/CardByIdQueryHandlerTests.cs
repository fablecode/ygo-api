using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.CardById;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardByIdQueryHandlerTests
    {
        private CardByIdQueryHandler _sut;
        private ICardService _cardService;

        [SetUp]
        public void SetUp()
        {
            _cardService = Substitute.For<ICardService>();

            _sut = new CardByIdQueryHandler(_cardService, new CardByIdQueryValidator());
        }

        [Test]
        public async Task Given_An_Invalid_Card_Name_Should_Return_Null()
        {
            // Arrange
            var query = new CardByIdQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_An_Valid_Query_Should_Execute_CardById()
        {
            // Arrange
            _cardService
                .CardById(Arg.Any<long>())
                .Returns(new Card());

            var query = new CardByIdQuery { Id = 696865 };

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _cardService.Received(1).CardById(Arg.Any<long>());
        }
    }
}