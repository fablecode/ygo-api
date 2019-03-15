using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.AllAttributes;
using ygo.application.Queries.AllCategories;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AllAttributesQueryHandlerTests
    {
        private IAttributeService _attributeService;
        private AllAttributesQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _attributeService = Substitute.For<IAttributeService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new AttributeProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new AllAttributesQueryHandler(_attributeService, mapper);
        }


        [Test]
        public async Task Given_An_AllAttributes_Query_Should_Return_All_Attributes()
        {
            // Arrange
            const int expected = 2;

            _attributeService.AllAttributes().Returns(new List<Attribute> { new Attribute(), new Attribute() });

            // Act
            var result = await _sut.Handle(new AllAttributesQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_An_AllAttributes_Query_Should_Invoke_AllAttributes_Method_Once()
        {
            // Arrange
            _attributeService.AllAttributes().Returns(new List<Attribute> { new Attribute(), new Attribute() });

            // Act
            await _sut.Handle(new AllAttributesQuery(), CancellationToken.None);

            // Assert
            await _attributeService.Received(1).AllAttributes();
        }

    }
}