using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands.AddCategory;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class AddCategoryCommandHandlerTests
    {
        private AddCategoryCommandHandler _sut;
        private ICategoryRepository _categoryRepository;

        [TestInitialize]
        public void Setup()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();

            _sut = new AddCategoryCommandHandler(_categoryRepository, new AddCategoryCommandValidator());
        }

        [TestMethod]
        public void Given_A_AddCategoryCommand_With_An_Invalid_Name_Should_Throw_ValidationException()
        {
            // Arrange
            var command = new AddCategoryCommand();

            // Act
            Action act = () => _sut.Handle(command);

            // Assert
            act.ShouldThrow<ValidationException>();
        }

        [TestMethod]
        public async Task Given_A_AddCategory_Command_With_An_Invalid_Name_Should_Not_Execute_Repository_AddCategory_Method()
        {
            // Arrange
            _categoryRepository
                .Add(Arg.Any<Category>())
                .Returns(new Category());

            var command = new AddCategoryCommand{ Name = "category"};

            // Act
            await  _sut.Handle(command);

            // Assert
            await _categoryRepository.DidNotReceive().Add(Arg.Any<Category>());
        }

    }
}