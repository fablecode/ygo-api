using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.AddCategory;
using ygo.application.Dto;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.CategoriesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class PostTests
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
        public async Task Given_An_AddCategoryCommand_If_Add_Category_Fails_Should_Return_BadRequest()
        {
            // Arrange
            _mediator.Send(Arg.Any<AddCategoryCommand>()).Returns(new CommandResult {Errors = new List<string> {"Name must have a value."}});

            // Act
            var result = await _sut.Post(new AddCategoryCommand());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Given_An_AddCategoryCommand_If_Add_Category_Fails_Should_Return_BadRequest_With_Errors()
        {
            // Arrange
            const string expected = "Name must have a value.";

            _mediator.Send(Arg.Any<AddCategoryCommand>()).Returns(new CommandResult {Errors = new List<string> {"Name must have a value."}});

            // Act
            var result = await _sut.Post(new AddCategoryCommand()) as BadRequestObjectResult;

            // Assert
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_An_AddCategoryCommand_If_Add_Category_Passes_Should_Return_CreatedAtRouteResult()
        {
            // Arrange
            _mediator.Send(Arg.Any<AddCategoryCommand>()).Returns(new CommandResult { IsSuccessful = true, Data = new CategoryDto() });

            // Act
            var result = await _sut.Post(new AddCategoryCommand { Name = "Monster"});

            // Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Test]
        public async Task Given_An_AddCategoryCommand_If_Add_Category_Passes_Should_Return_Invoke_AddCategoryCommand_Once()
        {
            // Arrange
            _mediator.Send(Arg.Any<AddCategoryCommand>()).Returns(new CommandResult { IsSuccessful = true, Data = new CategoryDto() });

            // Act
            await _sut.Post(new AddCategoryCommand { Name = "Monster"});

            // Assert
            await _mediator.Received(1).Send(Arg.Any<AddCategoryCommand>());
        }
    }
}