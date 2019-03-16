using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.BanlistById;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class BanlistByIdQueryHandlerTests
    {
        private BanlistByIdQueryHandler _sut;
        private IBanlistService _banlistService;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new BanlistProfile()); }
            );

            var mapper = config.CreateMapper();

            _banlistService = Substitute.For<IBanlistService>();
            _sut = new BanlistByIdQueryHandler(_banlistService, mapper);
        }

        [Test]
        public async Task Given_An_BanlistId_If_Not_Found_Should_Return_Null()
        {
            // Arrange
            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(null as Banlist);

            // Act
            var result = await _sut.Handle(new BanlistByIdQuery(), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_An_BanlistId_If_Found_Should_Return_Banlist()
        {
            // Arrange
            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(new Banlist());

            // Act
            var result = await _sut.Handle(new BanlistByIdQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }

        [Test]
        public async Task Given_An_BanlistId_If_Found_Should_Execute_GetBanlistById_Method_Once()
        {
            // Arrange
            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(new Banlist());

            // Act
            await _sut.Handle(new BanlistByIdQuery(), CancellationToken.None);

            // Assert
            await _banlistService.Received(1).GetBanlistById(Arg.Any<long>());
        }
    }
}