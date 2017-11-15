using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading.Tasks;
using ygo.application.Queries.ArchetypeByName;
using ygo.application.Queries.CardByName;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestClass]
    public class ArchetypeByNameQueryHandlerTests
    {
        private ArchetypeByNameQueryHandler _sut;
        private IArchetypeRepository _archetypeRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _archetypeRepository = Substitute.For<IArchetypeRepository>();

            _sut = new ArchetypeByNameQueryHandler(_archetypeRepository, new ArchetypeByNameQueryValidator());
        }

        [TestMethod]
        public async Task Given_An_Invalid_Archetype_Name_Should_Return_Null()
        {
            // Arrange
            var query = new ArchetypeByNameQuery();

            // Act
            var result = await _sut.Handle(query);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task Given_An_Valid_Query_Should_Execute_ArchetypeByName()
        {
            // Arrange
            _archetypeRepository
                .ArchetypeByName(Arg.Any<string>())
                .Returns(new Archetype());

            var query = new ArchetypeByNameQuery{ Name = "goodarchetypename"};

            // Act
            await _sut.Handle(query);

            // Assert
            await _archetypeRepository.Received(1).ArchetypeByName(Arg.Any<string>());
        }

    }
}