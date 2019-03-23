using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Dto;
using ygo.application.Queries.CategoryById;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.CategoriesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class GetByIdTests
    {
        private IMediator _mediator;
        private CategoriesController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _sut = new CategoriesController(_mediator);
        }

        [Test]
        public async Task Given_A_Category_Id_If_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            const int cardId = 523;

            // Act
            var result = await _sut.Get(cardId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Test]
        public async Task Given_A_Category_Id_If_Found_Should_Return_OkResult()
        {
            // Arrange
            const int categoryId = 523;

            _mediator.Send(Arg.Any<CategoryByIdQuery>()).Returns(new CategoryDto());

            // Act
            var result = await _sut.Get(categoryId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Given_A_Card_Id_If_Found_Should_Invoke_CardByIdQuery_Once()
        {
            // Arrange
            const int categoryId = 523;

            _mediator.Send(Arg.Any<CategoryByIdQuery>()).Returns(new CategoryDto());

            // Act
            await _sut.Get(categoryId);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<CategoryByIdQuery>());
        }
    }
}