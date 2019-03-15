using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Mappings.Profiles;
using ygo.application.Queries.AllTypes;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AllTypesQueryHandlerTests
    {
        private ITypeService _typeService;
        private AllTypesQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _typeService = Substitute.For<ITypeService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new TypeProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new AllTypesQueryHandler(_typeService, mapper);
        }


        [Test]
        public async Task Given_An_AllTypes_Query_Should_Return_All_Types()
        {
            // Arrange
            const int expected = 2;

            _typeService.AllTypes().Returns(new List<Type> { new Type(), new Type() });

            // Act
            var result = await _sut.Handle(new AllTypesQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected);
        }

        [Test]
        public async Task Given_An_AllTypes_Query_Should_Invoke_AllTypes_Method_Once()
        {
            // Arrange
            _typeService.AllTypes().Returns(new List<Type> { new Type(), new Type() });

            // Act
            await _sut.Handle(new AllTypesQuery(), CancellationToken.None);

            // Assert
            await _typeService.Received(1).AllTypes();
        }

    }
}