using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Enums;
using ygo.application.Queries.LatestBanlistByFormat;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.BanlistsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class LatestTests
    {
        private IMediator _mediator;
        private BanlistsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new BanlistsController(_mediator);
        }

        [Test]
        public async Task Given_A_Banlist_Format_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const BanlistFormat banlistFormat = BanlistFormat.Tcg;

            // Act
            var result = await _sut.Latest(banlistFormat);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_A_Banlist_Format__If_Found_Should_Return_OkResult()
        {
            // Arrange
            const BanlistFormat banlistFormat = BanlistFormat.Tcg;

            _mediator.Send(Arg.Any<LatestBanlistQuery>()).Returns(new LatestBanlistDto());

            // Act
            var result = await _sut.Latest(banlistFormat);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Banlist_Format_If_Found_Should_Invoke_BanlistByIdQuery_Once()
        {
            // Arrange
            const BanlistFormat banlistFormat = BanlistFormat.Tcg;

            _mediator.Send(Arg.Any<LatestBanlistQuery>()).Returns(new LatestBanlistDto());

            // Act
            await _sut.Latest(banlistFormat);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<LatestBanlistQuery>());
        }
    }
}