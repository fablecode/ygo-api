using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.api.Controllers;
using ygo.application.Queries.AllTypes;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class TypesControllerTests
    {
        private IMediator _mediator;
        private TypesController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _sut = new TypesController(_mediator);
        }

        [Test]
        public async Task Given_A_Get_AllTypes_Request_Should_Return_OkResult()
        {
            // Arrange

            // Act
            var result = await _sut.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Get_AllTypes_Request_Should_Invoke_AllTypesQuery_Once()
        {
            // Arrange

            // Act
            await _sut.Get();

            // Assert
            await _mediator.Received(1).Send(Arg.Any<AllTypesQuery>());
        }
    }
}