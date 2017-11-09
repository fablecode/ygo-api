using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.AddBanlist;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class AddBanlistCommandValidatorTests
    {
        private AddBanlistCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new AddBanlistCommandValidator();
        }

        [TestCase((long)-1)]
        [TestCase((long)0)]
        public void Given_An_Invalid_Id_Validation_Should_Fail(long id)
        {
            // Arrange
            var command = new AddBanlistCommand { Id = id };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Id, command);

            // Assert
            act.Invoke();
        }

        [TestCase((long)-1)]
        [TestCase((long)0)]
        public void Given_An_Invalid_FormatId_Validation_Should_Fail(long formatId)
        {
            // Arrange
            var command = new AddBanlistCommand { FormatId = formatId };

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
            var command = new AddBanlistCommand { Name = name };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.Name, command);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_An_Invalid_Release_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddBanlistCommand();

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(b => b.ReleaseDate, command);

            // Assert
            act.Invoke();
        }

        [Test]
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
