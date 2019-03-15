using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.ArchetypeById;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeByIdQueryHandlerTests
    {
        private IArchetypeService _archetypeService;
        private ArchetypeByIdQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _archetypeService = Substitute.For<IArchetypeService>();

            _sut = new ArchetypeByIdQueryHandler(_archetypeService);
        }


        [Test]
        public async Task Given_An_ArchetypeId_If_Not_Found_Should_Return_Null()
        {
            // Arrange
            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(null as Archetype);

            // Act
            var result = await _sut.Handle(new ArchetypeByIdQuery(), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task Given_An_ArchetypeId_If_Found_Should_Return_Archetype()
        {
            // Arrange
            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());

            // Act
            var result = await _sut.Handle(new ArchetypeByIdQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }

        [Test]
        public async Task Given_An_ArchetypeId_Should_Invoke_ArchetypeById_Once()
        {
            // Arrange
            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());

            // Act
            await _sut.Handle(new ArchetypeByIdQuery(), CancellationToken.None);

            // Assert
            await _archetypeService.Received(1).ArchetypeById(Arg.Any<long>());
        }
    }
}