using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Queries;
using ygo.application.Queries.ArchetypeImageById;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.ImagesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetArchetypeImageByIdTests
    {
        private IMediator _mediator;
        private ImagesController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _sut = new ImagesController(_mediator);
        }

        [Test]
        public async Task Given_An_Archetype_Id_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const long archetypeId = 98798;

            _mediator.Send(Arg.Any<ArchetypeImageByIdQuery>()).Returns(new ImageResult());

            // Act
            var result = await _sut.Get(archetypeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_An_Archetype_Id_If_Found_Should_Return_OkResult()
        {
            // Arrange
            const long archetypeId = 98798;

            _mediator.Send(Arg.Any<ArchetypeImageByIdQuery>()).Returns(new ImageResult { IsSuccessful = true, FilePath = @"c:\cards\images\image.png", ContentType = "image/png" });

            // Act
            var result = await _sut.Get(archetypeId);

            // Assert
            result.Should().BeOfType<PhysicalFileResult>();
        }

        [Test]
        public async Task Given_A_Card_Name_If_Found_Should_Invoke_CardByNameQuery_Once()
        {
            // Arrange
            const long archetypeId = 98798;

            _mediator.Send(Arg.Any<ArchetypeImageByIdQuery>()).Returns(new ImageResult { FilePath = @"c:\cards\images\image.png", ContentType = "image/png" });

            // Act
            await _sut.Get(archetypeId);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<ArchetypeImageByIdQuery>());
        }
    }
}