using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.AddCategory;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddCategoryCommandHandlerTests
    {
        private AddCategoryCommandHandler _sut;
        private ICategoryService _categoryService;

        [SetUp]
        public void SetUp()
        {
            _categoryService = Substitute.For<ICategoryService>();

            _sut = new AddCategoryCommandHandler(_categoryService, new AddCategoryCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_AddCategoryCommand_The_Command_Execution_Should_Be_Not_Successful()
        {
            // Arrange
            var command = new AddCategoryCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_AddCategoryCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new AddCategoryCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_AddCategoryCommand_Should_Not_Execute_Repository_AddCategory_Method()
        {
            // Arrange
            _categoryService
                .Add(Arg.Any<Category>())
                .Returns(new Category());

            var command = new AddCategoryCommand();

            // Act
            await  _sut.Handle(command, CancellationToken.None);

            // Assert
            await _categoryService.DidNotReceive().Add(Arg.Any<Category>());
        }

        [Test]
        public async Task Given_A_Valid_AddCategoryCommand_The_Command_Execution_Should_Be_Successfully()
        {
            // Arrange
            _categoryService
                .Add(Arg.Any<Category>())
                .Returns(new Category());

            var command = new AddCategoryCommand{ Name = "category"};

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

    }
}