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
    public class ArchetypeByNameTests
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
        public async Task Given_An_Archetype_Name_Should_Invoke_ArchetypeByName_Method_Once()
        {
            // Arrange
            const string archetypeName = "Toons";

            _archetypeRepository.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());

            // Act
            await _sut.ArchetypeByName(archetypeName);

            // Assert
            await _archetypeRepository.Received(1).ArchetypeByName(Arg.Is(archetypeName));
        }
    }
}