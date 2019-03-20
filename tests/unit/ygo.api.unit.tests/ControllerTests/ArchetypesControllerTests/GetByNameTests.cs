using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Queries.ArchetypeByName;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.ArchetypesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetByNameTests
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
        public async Task Given_An_Archetype_Name_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const string archetypeName = "archetype";

            // Act
            var result = await _sut.GetByName(archetypeName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_An_Archetype_Name_If_Found_Should_Return_OkObjectResult()
        {
            // Arrange
            const string archetypeName = "archetype";

            _mediator.Send(Arg.Any<ArchetypeByNameQuery>()).Returns(new ArchetypeDto());

            // Act
            var result = await _sut.GetByName(archetypeName);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_An_Archetype_Name_If_Found_Should_Invoke_Command_Once()
        {
            // Arrange
            const int expected = 1;
            const string archetypeName = "archetype";

            _mediator.Send(Arg.Any<ArchetypeByNameQuery>()).Returns(new ArchetypeDto());

            // Act
            await _sut.GetByName(archetypeName);

            // Assert
            await _mediator.Received(expected).Send(Arg.Any<ArchetypeByNameQuery>());
        }
    }
}