using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Commands.AddCard;
using ygo.domain.Enums;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestClass]
    public class AddCardCommandValidatorTests
    {
        private AddCardCommandValidator _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new AddCardCommandValidator();
        }

        //[DataTestMethod]
        //[DataRow(null)]
        //[DataRow("")]
        //[DataRow(" ")]
        //public void Given_An_Invalid_CardName_Validation_Should_Fail(string cardName)
        //{
        //    // Arrange
        //    var command = new AddCardCommand{ Name = cardName};

        //    // Act
        //    Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

        //    // Assert
        //    act.Invoke();
        //}

        //[TestMethod]
        //public void Given_A_CardName_If_Length_Is_Less_Than_2_Validation_Should_Fail()
        //{
        //    // Arrange
        //    var command = new AddCardCommand { Name = new string('c', 1) };

        //    // Act
        //    Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

        //    // Assert
        //    act.Invoke();
        //}

        //[TestMethod]
        //public void Given_A_CardName_If_Length_Is_Greater_Than_255_Validation_Should_Fail()
        //{
        //    // Arrange
        //    var command = new AddCardCommand { Name = new string('c', 256) };

        //    // Act
        //    Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

        //    // Assert
        //    act.Invoke();
        //}

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        public void Given_An_Invalid_CardType_Validation_Should_Fail(YgoCardType cardType)
        {
            // Arrange
            var command = new AddCardCommand{ CardType = cardType};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardType, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_Null_CardType_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddCardCommand();

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardType, command);

            // Assert
            act.Invoke();
        }


    }
}