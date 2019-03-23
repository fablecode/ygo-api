using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Queries.AllLinkArrows;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class LinkArrowsControllerTests
    {
        private IMediator _mediator;
        private LinkArrowsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _sut = new LinkArrowsController(_mediator);
        }

        [Test]
        public async Task Given_A_Get_AllLinkArrows_Request_Should_Return_OkResult()
        {
            // Arrange

            // Act
            var result = await _sut.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Get_AllLinkArrows_Request_Should_Invoke_AllLinkArrowsQuery_Once()
        {
            // Arrange

            // Act
            await _sut.Get();

            // Assert
            await _mediator.Received(1).Send(Arg.Any<AllLinkArrowsQuery>());
        }
    }
}