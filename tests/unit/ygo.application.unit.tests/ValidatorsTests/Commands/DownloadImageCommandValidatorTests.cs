using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.DownloadImage;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class DownloadImageCommandValidatorTests
    {
        private DownloadImageCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new DownloadImageCommandValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(@"C:\A\Big\Pile\Of\Poo")]
        public void Given_An_Invalid_ImageFolderPath_Validation_Should_Fail(string imageFilePath)
        {
            // Arrange
            var inputModel = new DownloadImageCommand { ImageFolderPath = imageFilePath };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(d => d.ImageFolderPath, inputModel);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_An_Invalid_RemoteImageUrl_Validation_Should_Fail()
        {
            // Arrange
            var inputModel = new DownloadImageCommand { RemoteImageUrl = null };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(d => d.RemoteImageUrl, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(@"*(&^")]
        public void Given_An_Invalid_ImageFileName_Validation_Should_Fail(string imageFileName)
        {
            // Arrange
            var inputModel = new DownloadImageCommand { ImageFileName = imageFileName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(d => d.ImageFileName, inputModel);

            // Assert
            act.Invoke();
        }

    }
}