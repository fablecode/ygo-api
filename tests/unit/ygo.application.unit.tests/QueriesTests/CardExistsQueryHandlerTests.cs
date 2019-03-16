using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.CardExists;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardExistsQueryHandlerTests
    {
        private ICardService _cardService;
        private CardExistsQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _cardService = Substitute.For<ICardService>();

            _sut = new CardExistsQueryHandler(_cardService);
        }

        [Test]
        public async Task Given_A_Banlist_Id_If_Should_Execute_BanlistExists_Method_Once()
        {
            // Arrange
            var query = new CardExistsQuery();

            _cardService.CardExists(Arg.Any<long>()).Returns(true);

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _cardService.Received(1).CardExists(Arg.Any<long>());
        }
    }
}