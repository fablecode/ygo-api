using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.AddArchetype;
using ygo.tests.core;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddArchetypeCommandValidatorTests
    {
        private AddArchetypeCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new AddArchetypeCommandValidator();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_ArchetypeNumber_Validation_Should_Fail(long archetypeNumber)
        {
            // Arrange
            var inputModel = new AddArchetypeCommand { ArchetypeNumber = archetypeNumber };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.ArchetypeNumber, inputModel);

            // Assert
            act.Invoke();
        }


        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_Name_Validation_Should_Fail(string name)
        {
            // Arrange
            var inputModel = new AddArchetypeCommand { Name = name};

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
            var inputModel = new AddArchetypeCommand { ProfileUrl = url};

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
            var inputModel = new AddArchetypeCommand { ImageUrl = url };

            // Act
            Action act = () => _sut.ShouldNotHaveValidationErrorFor(a => a.ImageUrl, inputModel);

            // Assert
            act.Invoke();
        }
    }
}