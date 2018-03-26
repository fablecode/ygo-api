﻿using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo.application.Commands.AddCategory;
using ygo.core.Models.Db;
using ygo.domain.Repository;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    public class AddCategoryCommandHandlerTests
    {
        private AddCategoryCommandHandler _sut;
        private ICategoryRepository _categoryRepository;

        [SetUp]
        public void SetUp()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();

            _sut = new AddCategoryCommandHandler(_categoryRepository, new AddCategoryCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_AddCategoryCommand_The_Command_Execution_Should_Be_Not_Successful()
        {
            // Arrange
            var command = new AddCategoryCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_AddCategoryCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new AddCategoryCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_AddCategoryCommand_Should_Not_Execute_Repository_AddCategory_Method()
        {
            // Arrange
            _categoryRepository
                .Add(Arg.Any<Category>())
                .Returns(new Category());

            var command = new AddCategoryCommand();

            // Act
            await  _sut.Handle(command);

            // Assert
            await _categoryRepository.DidNotReceive().Add(Arg.Any<Category>());
        }

        [Test]
        public async Task Given_A_Valid_AddCategoryCommand_The_Command_Execution_Should_Be_Successfully()
        {
            // Arrange
            _categoryRepository
                .Add(Arg.Any<Category>())
                .Returns(new Category());

            var command = new AddCategoryCommand{ Name = "category"};

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

    }
}