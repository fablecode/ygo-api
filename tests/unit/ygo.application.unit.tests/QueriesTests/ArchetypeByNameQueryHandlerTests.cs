using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.ArchetypeByName;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeByNameQueryHandlerTests
    {
        private ArchetypeByNameQueryHandler _sut;
        private IArchetypeService _archetypeService;

        [SetUp]
        public void SetUp()
        {
            _archetypeService = Substitute.For<IArchetypeService>();

            _sut = new ArchetypeByNameQueryHandler(_archetypeService, new ArchetypeByNameQueryValidator());
        }

        [Test]
        public async Task Given_An_Invalid_Archetype_Name_Should_Return_Null()
        {
            // Arrange
            var query = new ArchetypeByNameQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_An_Valid_Query_Should_Execute_ArchetypeByName()
        {
            // Arrange
            _archetypeService
                .ArchetypeByName(Arg.Any<string>())
                .Returns(new Archetype());

            var query = new ArchetypeByNameQuery{ Name = "goodarchetypename"};

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _archetypeService.Received(1).ArchetypeByName(Arg.Any<string>());
        }

    }
}