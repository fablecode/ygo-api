using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.api.Controllers;
using ygo.application.Queries;
using ygo.application.Queries.CardImageByName;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.ImagesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetCardImageByNameTests
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
        public async Task Given_A_Card_Name_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const string cardName = "Call Of The Haunted";
            _mediator.Send(Arg.Any<CardImageByNameQuery>()).Returns(new ImageResult());

            // Act
            var result = await _sut.Get(cardName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task Given_A_Card_Name_If_Found_Should_Return_OkResult()
        {
            // Arrange
            const string cardName = "Call Of The Haunted";

            _mediator.Send(Arg.Any<CardImageByNameQuery>()).Returns(new ImageResult { IsSuccessful = true, FilePath = @"c:\cards\images\image.png", ContentType = "image/png" });

            // Act
            var result = await _sut.Get(cardName);

            // Assert
            result.Should().BeOfType<PhysicalFileResult>();
        }

        [Test]
        public async Task Given_A_Card_Name_If_Found_Should_Invoke_CardByNameQuery_Once()
        {
            // Arrange
            const string cardName = "Call Of The Haunted";

            _mediator.Send(Arg.Any<CardImageByNameQuery>()).Returns(new ImageResult { FilePath = @"c:\cards\images\image.png", ContentType = "image/png" });

            // Act
            await _sut.Get(cardName);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<CardImageByNameQuery>());
        }
    }
}