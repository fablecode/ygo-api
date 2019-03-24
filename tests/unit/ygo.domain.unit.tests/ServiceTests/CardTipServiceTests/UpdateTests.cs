using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.core.Models.Db;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardTipServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateTests
    {
        private ICardTipRepository _cardTipRepository;
        private CardTipService _sut;

        [SetUp]
        public void SetUp()
        {
            _cardTipRepository = Substitute.For<ICardTipRepository>();
            _sut = new CardTipService(_cardTipRepository);
        }

        [Test]
        public async Task Given_TipSections_Should_Invoke_Update_Once()
        {
            // Arrange
            var tipSections = new List<TipSection>();

            // Act
            await _sut.Update(tipSections);

            // Assert
            await _cardTipRepository.Update(Arg.Is(tipSections));
        }
    }
}