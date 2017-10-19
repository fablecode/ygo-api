using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ygo.application.Commands.AddBanlist;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestClass]
    public class AddBanlistCommandValidatorTests
    {
        private AddBanlistCommandValidator _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new AddBanlistCommandValidator();
        }

        [DataTestMethod]
        [DataRow((long)-1)]
        [DataRow((long)0)]
        public void Given_An_Invalid_Id_Validation_Should_Fail(long id)
        {
            // Arrange
            var command = new AddBanlistCommand { Id = id };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Id, command);

            // Assert
            act.Invoke();
        }

        [DataTestMethod]
        [DataRow((long)-1)]
        [DataRow((long)0)]
        public void Given_An_Invalid_FormatId_Validation_Should_Fail(long formatId)
        {
            // Arrange
            var command = new AddBanlistCommand { FormatId = formatId };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.FormatId, command);

            // Assert
            act.Invoke();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Invalid_Name_Validation_Should_Fail(string name)
        {
            // Arrange
            var command = new AddBanlistCommand { Name = name };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Name, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_An_Name_Greater_Than_255_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddBanlistCommand { Name = new string('*', 256) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Name, command);

            // Assert
            act.Invoke();
        }
    }
}