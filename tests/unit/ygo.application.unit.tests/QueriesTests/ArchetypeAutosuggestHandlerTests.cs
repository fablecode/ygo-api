using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.ArchetypeAutosuggest;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeAutosuggestHandlerTests
    {
        private IArchetypeService _archetypeService;
        private ArchetypeAutosuggestHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _archetypeService = Substitute.For<IArchetypeService>();

            _sut = new ArchetypeAutosuggestHandler(_archetypeService);
        }


        [Test]
        public async Task Given_A_Filter_Should_Archetype_Names()
        {
            // Arrange
            const int expected = 2;

            _archetypeService.Names(Arg.Any<string>()).Returns(new List<string>{ "Toons", "Dark Magician"});

            // Act
            var result = await _sut.Handle(new ArchetypeAutosuggestQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_A_Filter_Should_Invoke_Names_Method_Once()
        {
            // Arrange
            _archetypeService.Names(Arg.Any<string>()).Returns(new List<string> { "Toons", "Dark Magician" });

            // Act
            await _sut.Handle(new ArchetypeAutosuggestQuery(), CancellationToken.None);

            // Assert
            await _archetypeService.Received(1).Names(Arg.Any<string>());
        }

    }
}