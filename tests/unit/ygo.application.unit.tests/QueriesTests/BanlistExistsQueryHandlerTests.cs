using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.BanlistExists;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class BanlistExistsQueryHandlerTests
    {
        private IBanlistService _banlistService;
        private BanlistExistsQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _banlistService = Substitute.For<IBanlistService>();

            _sut = new BanlistExistsQueryHandler(_banlistService);
        }

        [Test]
        public async Task Given_A_Banlist_Id_If_Should_Execute_BanlistExists_Method_Once()
        {
            // Arrange
            var query = new BanlistExistsQuery();

            _banlistService.BanlistExist(Arg.Any<long>()).Returns(true);

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _banlistService.Received(1).BanlistExist(Arg.Any<long>());
        }
    }
}