using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Queries.BanlistById;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.BanlistsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetTests
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
        public async Task Given_A_Banlist_Id_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const int banlistId = 523;

            // Act
            var result = await _sut.Get(banlistId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_A_Banlist_Id_If_Found_Should_Return_OkResult()
        {
            // Arrange
            const int banlistId = 523;

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns(new BanlistDto());

            // Act
            var result = await _sut.Get(banlistId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Banlist_Id_If_Found_Should_Invoke_BanlistByIdQuery_Once()
        {
            // Arrange
            const int banlistId = 523;

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns(new BanlistDto());

            // Act
            await _sut.Get(banlistId);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<BanlistByIdQuery>());
        }
    }
}