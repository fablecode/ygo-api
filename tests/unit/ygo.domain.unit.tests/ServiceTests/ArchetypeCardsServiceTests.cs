using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests
{

    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeCardsServiceTests
    {
        private IArchetypeCardsRepository _archetypeCardsRepository;
        private ArchetypeCardsService _sut;

        [SetUp]
        public void SetUp()
        {
            _archetypeCardsRepository = Substitute.For<IArchetypeCardsRepository>();
            _sut = new ArchetypeCardsService(_archetypeCardsRepository);
        }

        [Test]
        public async Task Given_An_ArchetypeId_And_Cards_Should_Invoke_Update_Method_Once()
        {
            // Arrange
            const int archetypeId = 423;
            var cards = new List<string>();

            _archetypeCardsRepository.Update(Arg.Any<long>(), Arg.Any<List<string>>()).Returns(new List<ArchetypeCard>());

            // Act
            await _sut.Update(archetypeId, cards);

            // Assert
            await _archetypeCardsRepository.Received(1).Update(Arg.Is<long>(archetypeId), Arg.Is(cards));
        }
    }
}