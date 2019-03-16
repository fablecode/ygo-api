using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.application.Paging;
using ygo.application.Queries.ArchetypeSearch;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeSearchQueryHandlerTests
    {
        private IArchetypeService _archetypeService;
        private ArchetypeSearchQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _archetypeService = Substitute.For<IArchetypeService>();

            _sut = new ArchetypeSearchQueryHandler(_archetypeService, new ArchetypeSearchQueryValidator());
        }

        [Test]
        public async Task Given_An_Invalid_ArchetypeSearchQuery_Should_Not_Execute_Successfully()
        {
            // Arrange
            var query = new ArchetypeSearchQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_ArchetypeSearchQuery_Should_Return_Error_List()
        {
            // Arrange
            var query = new ArchetypeSearchQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_A_Valid_ArchetypeSearchQuery_Should_Execute_Successfully()
        {
            // Arrange
            var query = new ArchetypeSearchQuery
            {
                PageNumber = 1,
                PageSize = 10
            };

            _archetypeService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Archetype>{ Items = new List<Archetype>()});

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_A_Valid_ArchetypeSearchQuery_Data_Should_Be_Of_Type_PagedList()
        {
            // Arrange
            var query = new ArchetypeSearchQuery
            {
                PageNumber = 1,
                PageSize = 10
            };

            _archetypeService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Archetype>{ Items = new List<Archetype>{new Archetype()}});

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().BeOfType<PagedList<ArchetypeDto>>();
        }


        [Test]
        public async Task Given_A_Valid_ArchetypeSearchQuery_Data_Should_Archetype_List()
        {
            // Arrange
            const int expected = 2;

            var query = new ArchetypeSearchQuery
            {
                PageNumber = 1,
                PageSize = 10
            };

            _archetypeService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Archetype>{ Items = new List<Archetype>
            {
                new Archetype(),
                new Archetype()
            }});

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            var pagedList =  (PagedList<ArchetypeDto>)result.Data;
            pagedList.List.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_A_Valid_ArchetypeSearchQuery_Should_Execute_Search_Method_Once()
        {
            // Arrange
            var query = new ArchetypeSearchQuery
            {
                PageNumber = 1,
                PageSize = 10
            };

            _archetypeService.Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new SearchResult<Archetype>{ Items = new List<Archetype>()});

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _archetypeService.Received(1).Search(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());
        }
    }
}