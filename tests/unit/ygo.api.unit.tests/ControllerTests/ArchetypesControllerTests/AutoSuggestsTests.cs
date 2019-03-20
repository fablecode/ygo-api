using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Queries.ArchetypeAutosuggest;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.ArchetypesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AutoSuggestsTests
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
        public async Task Given_An_Archetype_Filter_If_Archetypes_Are_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const string filter = "toons";

            _mediator.Send(Arg.Any<ArchetypeAutosuggestQuery>()).Returns((IEnumerable<string>) null);

            // Act
            var result = await _sut.AutoSuggests(filter);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_An_Archetype_Filter_If_Archetypes_Are_Found_Should_Return_OkObjectResult()
        {
            // Arrange
            const string filter = "toons";

            _mediator.Send(Arg.Any<ArchetypeAutosuggestQuery>()).Returns(new List<string>());

            // Act
            var result = await _sut.AutoSuggests(filter);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_An_Archetype_Filter_If_Archetypes_Are_Found_Should_Invoke_Command_Once()
        {
            // Arrange
            const int expected = 1;
            const string archetypeName = "archetype";

            _mediator.Send(Arg.Any<ArchetypeAutosuggestQuery>()).Returns(new List<string>());

            // Act
            await _sut.AutoSuggests(archetypeName);

            // Assert
            await _mediator.Received(expected).Send(Arg.Any<ArchetypeAutosuggestQuery>());
        }
    }
}