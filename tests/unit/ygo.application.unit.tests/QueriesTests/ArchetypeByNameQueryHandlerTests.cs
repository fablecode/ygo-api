using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.ArchetypeByName;
using ygo.domain.Repository;
using ygo.infrastructure.Models;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    public class ArchetypeByNameQueryHandlerTests
    {
        private ArchetypeByNameQueryHandler _sut;
        private IArchetypeRepository _archetypeRepository;

        [SetUp]
        public void SetUp()
        {
            _archetypeRepository = Substitute.For<IArchetypeRepository>();

            _sut = new ArchetypeByNameQueryHandler(_archetypeRepository, new ArchetypeByNameQueryValidator());
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
            _archetypeRepository
                .ArchetypeByName(Arg.Any<string>())
                .Returns(new Archetype());

            var query = new ArchetypeByNameQuery{ Name = "goodarchetypename"};

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _archetypeRepository.Received(1).ArchetypeByName(Arg.Any<string>());
        }

    }
}