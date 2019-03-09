﻿using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.CardByName;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardByNameQueryHandlerTests
    {
        private CardByNameQueryHandler _sut;
        private ICardService _cardService;

        [SetUp]
        public void SetUp()
        {
            _cardService = Substitute.For<ICardService>();

            _sut = new CardByNameQueryHandler(_cardService, new CardByNameQueryValidator());
        }

        [Test]
        public async Task Given_An_Invalid_Card_Name_Should_Return_Null()
        {
            // Arrange
            var query = new CardByNameQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_An_Valid_Query_Should_Execute_CardByName()
        {
            // Arrange
            _cardService
                .CardByName(Arg.Any<string>())
                .Returns(new Card());

            var query = new CardByNameQuery{ Name = "goodcardname"};

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _cardService.Received(1).CardByName(Arg.Any<string>());
        }

    }
}