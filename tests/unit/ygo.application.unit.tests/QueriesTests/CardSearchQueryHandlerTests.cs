using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ygo.application.Configuration;
using ygo.application.Dto;
using ygo.application.Mappings.Resolvers;
using ygo.application.Paging;
using ygo.application.Queries.CardSearch;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardSearchQueryHandlerTests
    {
        private ICardService _cardService;
        private CardSearchQueryHandler _sut;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = Substitute.For<IMapper>();

            _cardService = Substitute.For<ICardService>();

            _sut = new CardSearchQueryHandler(_cardService, new CardSearchQueryValidator(), _mapper);
        }

        [Test]
        public async Task Given_An_Invalid_CardSearchQuery_Should_Not_Execute_Successfully()
        {
            // Arrange
            var query = new CardSearchQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_CardSearchQuery_Should_Return_Error_List()
        {
            // Arrange
            var query = new CardSearchQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_A_Valid_CardSearchQuery_Should_Execute_Successfully()
        {
            // Arrange
            var query = new CardSearchQuery
            {
                PageIndex = 1,
                PageSize = 10
            };

            _cardService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Card> { Items = new List<Card>()});

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_A_Valid_CardSearchQuery_Data_Should_Be_Of_Type_PagedList()
        {
            // Arrange
            var query = new CardSearchQuery
            {
                PageIndex = 1,
                PageSize = 10
            };

            _mapper.Map<CardImageEndpointResolver>(Arg.Any<Card>()).Returns(new CardImageEndpointResolver(Substitute.For<IOptions<ApplicationSettings>>()));

            _cardService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Card>{ Items = new List<Card>{new Card()}});

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().BeOfType<PagedList<CardDto>>();
        }


        [Test]
        public async Task Given_A_Valid_CardSearchQuery_Data_Should_Archetype_List()
        {
            // Arrange
            const int expected = 2;

            var query = new CardSearchQuery
            {
                PageIndex = 1,
                PageSize = 10
            };

            _cardService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Card>{ Items = new List<Card>
            {
                new Card(),
                new Card()
            }});

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            var pagedList =  (PagedList<CardDto>)result.Data;
            pagedList.List.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_A_Valid_CardSearchQuery_Should_Execute_Search_Method_Once()
        {
            // Arrange
            var query = new CardSearchQuery
            {
                PageIndex = 1,
                PageSize = 10
            };

            _cardService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Card> { Items = new List<Card>()});

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _cardService.Received(1).Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());
        }
    }
}