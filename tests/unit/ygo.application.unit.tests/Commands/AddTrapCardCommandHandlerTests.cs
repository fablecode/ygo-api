﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Repository;
using ygo.domain.Models;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class AddTrapCardCommandHandlerTests
    {
        private AddTrapCardCommandHandler _sut;
        private ICardRepository _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = Substitute.For<ICardRepository>();

            _sut = new AddTrapCardCommandHandler(_repository, new AddTrapCardCommandValidator());
        }

        [TestMethod]
        public async Task Given_An_Invalid_AddTrapCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new AddTrapCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Given_An_Invalid_AddTrapCardCommand_Should_Not_Execute_AddCard()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = new AddTrapCardCommand();

            // Act
            await _sut.Handle(command);

            // Assert
            _repository.DidNotReceive();
        }

        [TestMethod]
        public async Task Given_An_Valid_AddTrapCardCommand_Should_Execute_AddCard()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = GetValidTrapCard();

            // Act
            await _sut.Handle(command);

            // Assert
            await _repository.Received(1).Add(Arg.Any<Card>());
        }

        [TestMethod]
        public async Task Given_An_Valid_AddTrapCardCommand_ISuccessful_Flag_Should_True()
        {
            // Arrange
            _repository.Add(Arg.Any<Card>()).Returns(new Card());
            var command = GetValidTrapCard();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        private static AddTrapCardCommand GetValidTrapCard()
        {
            return new AddTrapCardCommand
            {
                Name = "Call of the Haunted",
                SubCategoryIds = new List<int>
                {
                    22 // Continuous Trap
                },
                Description = "Activate this card by targeting 1 monster in your GY; Special Summon that target in Attack Position. When this card leaves the field, destroy that target. When that target is destroyed, destroy this card.",
                CardNumber = 97077563
            };
        }
    }
}