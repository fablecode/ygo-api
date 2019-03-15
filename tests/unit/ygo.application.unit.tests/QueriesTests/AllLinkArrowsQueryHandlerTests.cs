using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.AllLinkArrows;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AllLinkArrowsQueryHandlerTests
    {
        private ILinkArrowService _linkArrowService;
        private AllLinkArrowsQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _linkArrowService = Substitute.For<ILinkArrowService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new LinkArrowProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new AllLinkArrowsQueryHandler(_linkArrowService, mapper);
        }


        [Test]
        public async Task Given_An_AllLinkArrows_Query_Should_Return_All_LinkArrows()
        {
            // Arrange
            const int expected = 2;

            _linkArrowService.AllLinkArrows().Returns(new List<LinkArrow> { new LinkArrow(), new LinkArrow() });

            // Act
            var result = await _sut.Handle(new AllLinkArrowsQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_An_AllLinkArrows_Query_Should_Invoke_AllLinkArrows_Method_Once()
        {
            // Arrange
            _linkArrowService.AllLinkArrows().Returns(new List<LinkArrow> { new LinkArrow(), new LinkArrow() });

            // Act
            await _sut.Handle(new AllLinkArrowsQuery(), CancellationToken.None);

            // Assert
            await _linkArrowService.Received(1).AllLinkArrows();
        }

    }
}