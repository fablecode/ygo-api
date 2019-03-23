﻿using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ygo.domain.Repository;
using ygo.domain.Services;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.CardRulingServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class DeleteByCardIdTests
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
        public async Task Given_A_CardId_Should_Invoke_DeleteByCardId_Method_Once()
        {
            // Arrange
            const long cardId = 9043;

            // Act
            await _sut.DeleteByCardId(cardId);

            // Assert
            await _cardRulingRepository.DeleteByCardId(Arg.Is(cardId));
        }
    }
}