using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Enums;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.CategoryById;
using ygo.application.Queries.FormatByAcronym;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class FormatByAcronymQueryHandlerTests
    {
        private FormatByAcronymQueryHandler _sut;
        private IFormatService _formatService;

        [SetUp]
        public void SetUp()
        {
            _formatService = Substitute.For<IFormatService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new FormatProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new FormatByAcronymQueryHandler(_formatService, mapper);
        }

        [Test]
        public async Task Given_An_Invalid_Query_Should_Return_Null()
        {
            // Arrange
            var query = new FormatByAcronymQuery();

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_A_Valid_Query_Should_Execute_FormatByAcronym_Once()
        {
            // Arrange
            _formatService
                .FormatByAcronym(Arg.Any<string>())
                .Returns(new Format());

            var query = new FormatByAcronymQuery { Acronym = BanlistFormat.Tcg};

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _formatService.Received(1).FormatByAcronym(Arg.Any<string>());
        }

    }
}