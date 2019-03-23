using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.ArchetypeServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class NamesTests
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
        public async Task Given_A_Filter_Should_Invoke_Names_Method_Once()
        {
            // Arrange
            const string filter = "blue-eye";

            _archetypeRepository.Names(Arg.Any<string>()).Returns(new List<string>());

            // Act
            await _sut.Names(filter);

            // Assert
            await _archetypeRepository.Received(1).Names(Arg.Is(filter));
        }
    }
}