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
    public class ArchetypeByIdTests
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
        public async Task Given_An_Archetype_Id_Should_Invoke_ArchetypeById_Method_Once()
        {
            // Arrange
            const long archetypeId = 424;

            _archetypeRepository.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());

            // Act
            await _sut.ArchetypeById(archetypeId);

            // Assert
            await _archetypeRepository.Received(1).ArchetypeById(Arg.Is(archetypeId));
        }
    }
}