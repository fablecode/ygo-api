using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.AllLimits;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AllLimitQueryHandlerTests
    {
        private ILimitService _limitService;
        private AllLimitsQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _limitService = Substitute.For<ILimitService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new AttributeProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new AllLimitsQueryHandler(_limitService, mapper);
        }


        [Test]
        public async Task Given_An_AllLimits_Query_Should_Return_All_Limits()
        {
            // Arrange
            const int expected = 2;

            _limitService.AllLimits().Returns(new List<Limit> { new Limit(), new Limit() });

            // Act
            var result = await _sut.Handle(new AllLimitsQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_An_AllLimits_Query_Should_Invoke_AllLimits_Method_Once()
        {
            // Arrange
            _limitService.AllLimits().Returns(new List<Limit> { new Limit(), new Limit() });

            // Act
            await _sut.Handle(new AllLimitsQuery(), CancellationToken.None);

            // Assert
            await _limitService.Received(1).AllLimits();
        }

    }
}