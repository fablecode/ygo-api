using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.ArchetypeServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class SearchTests
    {
        private IArchetypeRepository _archetypeRepository;
        private ArchetypeService _sut;

        [SetUp]
        public void SetUp()
        {
            _archetypeRepository = Substitute.For<IArchetypeRepository>();
            _sut = new ArchetypeService(_archetypeRepository);
        }

        [Test]
        public async Task Given_Search_Parameters_Should_Invoke_Search_Method_Once()
        {
            // Arrange
            const string searchTerm = "toon";
            const int pageNumber = 2;
            const int pageSize = 10;

            _archetypeRepository.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Archetype>());

            // Act
            await _sut.Search(searchTerm, pageNumber, pageSize);

            // Assert
            await _archetypeRepository.Received(1).Search(Arg.Is(searchTerm), Arg.Is(pageNumber), Arg.Is(pageSize));
        }
    }
}