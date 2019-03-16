using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Enums;
using ygo.application.Queries.LatestBanlistByFormat;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class LatestBanlistQueryHandlerTests
    {
        private LatestBanlistQueryHandler _sut;
        private IBanlistService _banlistService;

        [SetUp]
        public void SetUp()
        {
            _banlistService = Substitute.For<IBanlistService>();

            _sut = new LatestBanlistQueryHandler(_banlistService);
        }

        [Test]
        public async Task Given_An_Invalid_Query_Forbidden_Limited_And_SemiLimited_Should_Be_Null()
        {
            // Arrange
            var query = new LatestBanlistQuery();

            _banlistService
                .GetBanlistByFormatAcronym(Arg.Any<string>())
                .Returns(new Banlist
                {
                    Format = new Format
                    {
                        Acronym = "TCG"
                    }
                });

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Forbidden.Should().BeNull();
            result.Limited.Should().BeNull();
            result.SemiLimited.Should().BeNull();
        }

        [Test]
        public async Task Given_A_Valid_Query_Should_Execute_GetBanlistByFormatAcronym_Once()
        {
            // Arrange
            _banlistService
                .GetBanlistByFormatAcronym(Arg.Any<string>())
                .Returns(new Banlist
                {
                    Format = new Format
                    {
                        Acronym = "TCG"
                    }
                });

            var query = new LatestBanlistQuery { Acronym = BanlistFormat.Tcg};

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _banlistService.Received(1).GetBanlistByFormatAcronym(Arg.Any<string>());
        }

    }
}