using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardRulingServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateTests
    {
        private ICardRulingRepository _cardRulingRepository;
        private CardRulingService _sut;

        [SetUp]
        public void SetUp()
        {
            _cardRulingRepository = Substitute.For<ICardRulingRepository>();
            _sut = new CardRulingService(_cardRulingRepository);
        }

        [Test]
        public async Task Given_RulingSections_Should_Invoke_Update_Method_Once()
        {
            // Arrange
            var rulingSections = new List<RulingSection>();

            // Act
            await _sut.Update(rulingSections);

            // Assert
            await _cardRulingRepository.Update(Arg.Is(rulingSections));
        }
    }
}