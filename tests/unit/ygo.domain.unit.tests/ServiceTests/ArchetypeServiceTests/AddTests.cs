using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.ArchetypeServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddTests
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
        public async Task Given_An_Archetype_Should_Invoke_Add_Method_Once()
        {
            // Arrange
            var archetype = new Archetype();

            _archetypeRepository.Add(Arg.Any<Archetype>()).Returns(new Archetype());

            // Act
            await _sut.Add(archetype);

            // Assert
            await _archetypeRepository.Received(1).Add(Arg.Is(archetype));
        }
    }
}