using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.UpdateBanlist;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class UpdateBanlistCommandValidatorTests
    {
        private UpdateBanlistCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateBanlistCommandValidator();
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Given_An_Invalid_Id_Validation_Should_Fail(long id)
        {
            // Arrange
            var command = new UpdateBanlistCommand { Id = id };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Id, command);

            // Assert
            act.Invoke();
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Given_An_Invalid_FormatId_Validation_Should_Fail(long formatId)
        {
            // Arrange
            var command = new UpdateBanlistCommand { FormatId = formatId };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.FormatId, command);

            // Assert
            act.Invoke();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_Name_Validation_Should_Fail(string name)
        {
            // Arrange
            var command = new UpdateBanlistCommand { Name = name };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_An_Invalid_Release_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateBanlistCommand();

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.ReleaseDate, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_An_Name_Greater_Than_255_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateBanlistCommand { Name = new string('*', 256) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Name, command);

            // Assert
            act.Invoke();
        }
    }
}