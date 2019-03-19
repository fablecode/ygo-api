using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Queries.ArchetypeById;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.ArchetypesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetByIdTests
    {
        private IMediator _mediator;
        private ArchetypesController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new ArchetypesController(_mediator);
        }

        [Test]
        public async Task Given_A_ArchetypeId_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            var archetypeId = 2342;

            // Act
            var result = await _sut.GetById(archetypeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_A_ArchetypeId_If_Found_Should_Return_OkObjectResult()
        {
            // Arrange
            var archetypeId = 2342;

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns(new ArchetypeDto());

            // Act
            var result = await _sut.GetById(archetypeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }


        [Test]
        public async Task Given_A_ArchetypeId_If_Found_Should_Invoke_Command_Once()
        {
            // Arrange
            var expected = 1;
            var archetypeId = 2342;

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns(new ArchetypeDto());

            // Act
            await _sut.GetById(archetypeId);

            // Assert
            await _mediator.Received(expected).Send(Arg.Any<ArchetypeByIdQuery>());
        }
    }
}