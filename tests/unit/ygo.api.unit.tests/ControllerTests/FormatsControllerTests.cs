using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.api.Controllers;
using ygo.application.Enums;
using ygo.application.Queries.FormatByAcronym;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class FormatsControllerTests
    {
        private IMediator _mediator;
        private FormatsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new FormatsController(_mediator);
        }

        [Test]
        public async Task Given_A_Format_Acronym_Should_Return_OkResult()
        {
            // Arrange
            const BanlistFormat banlistFormat = BanlistFormat.Tcg;

            // Act
            var result = await _sut.Get(banlistFormat);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Format_Acronym_Should_Invoke_FormatByAcronymQuery_Once()
        {
            // Arrange
            const BanlistFormat banlistFormat = BanlistFormat.Tcg;

            // Act
            await _sut.Get(banlistFormat);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<FormatByAcronymQuery>());
        }
    }
}