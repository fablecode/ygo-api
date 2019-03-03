using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.UpdateArchetype;
using ygo.tests.core;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateArchetypeCommandValidatorTests
    {
        private UpdateArchetypeCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UpdateArchetypeCommandValidator();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_ArchetypeNumber_Validation_Should_Fail(long Id)
        {
            // Arrange
            var inputModel = new UpdateArchetypeCommand { Id = Id };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.Id, inputModel);

            // Assert
            act.Invoke();
        }


        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_Name_Validation_Should_Fail(string name)
        {
            // Arrange
            var inputModel = new UpdateArchetypeCommand { Name = name};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.Name, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("ftp://billgates.microsoft.com/special/secretplans")]
        public void Given_An_Invalid_ProfileUrl_Validation_Should_Fail(string url)
        {
            // Arrange
            var inputModel = new UpdateArchetypeCommand { ProfileUrl = url};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.ProfileUrl, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_ImageUrl_Validation_Should_Fail(string url)
        {
            // Arrange
            var inputModel = new UpdateArchetypeCommand { ImageUrl = url };

            // Act
            Action act = () => _sut.ShouldNotHaveValidationErrorFor(a => a.ImageUrl, inputModel);

            // Assert
            act.Invoke();
        }
    }
}