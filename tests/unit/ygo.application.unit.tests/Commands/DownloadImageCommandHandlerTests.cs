using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Commands.DownloadImage;
using ygo.core.Models;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class DownloadImageCommandHandlerTests
    {
        private DownloadImageCommandHandler _sut;
        private IFileSystemService _fileSystemService;

        [SetUp]
        public void SetUp()
        {
            _fileSystemService = Substitute.For<IFileSystemService>();
            _sut = new DownloadImageCommandHandler(_fileSystemService, new DownloadImageCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_DownloadImageCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new DownloadImageCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_DownloadImageCommand_Validation_Should_Return_Errors_List()
        {
            // Arrange
            var command = new DownloadImageCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }


        [Test]
        public async Task Given_A_Valid_DownloadImageCommand_And_The_ImageFile_Does_Not_Exist_Should_Not_Execute_Delete_Method()
        {
            // Arrange
            var command = new DownloadImageCommand
            {
                ImageFolderPath = @"c:\windows",
                ImageFileName = "Call Of The Haunted",
                RemoteImageUrl = new Uri("http://filesomewhere/callofthehaunted.png")
            };

            _fileSystemService.Download(Arg.Any<Uri>(), Arg.Any<string>()).Returns(new DownloadedFile {ContentType = "image/png"});

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            _fileSystemService.DidNotReceive().Delete(Arg.Any<string>());
        }

        [Test]
        public async Task Given_A_Valid_DownloadImageCommand_And_The_ImageFile_Exists_Should_Execute_Delete_Method()
        {
            // Arrange
            var command = new DownloadImageCommand
            {
                ImageFolderPath = @"c:\windows",
                ImageFileName = "Call Of The Haunted",
                RemoteImageUrl = new Uri("http://filesomewhere/callofthehaunted.png")
            };

            _fileSystemService.Download(Arg.Any<Uri>(), Arg.Any<string>()).Returns(new DownloadedFile {ContentType = "image/png"});
            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).Delete(Arg.Any<string>());
        }

        [Test]
        public async Task Given_A_Valid_DownloadImageCommand_Should_Execute_Download_Method_Once()
        {
            // Arrange
            var command = new DownloadImageCommand
            {
                ImageFolderPath = @"c:\windows",
                ImageFileName = "Call Of The Haunted",
                RemoteImageUrl = new Uri("http://filesomewhere/callofthehaunted.png")
            };

            _fileSystemService.Download(Arg.Any<Uri>(), Arg.Any<string>()).Returns(new DownloadedFile {ContentType = "image/png"});
            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _fileSystemService.Received(1).Download(Arg.Any<Uri>(), Arg.Any<string>());
        }

        [Test]
        public async Task Given_A_Valid_DownloadImageCommand_Should_Execute_Download_Rename_Once()
        {
            // Arrange
            var command = new DownloadImageCommand
            {
                ImageFolderPath = @"c:\windows",
                ImageFileName = "Call Of The Haunted",
                RemoteImageUrl = new Uri("http://filesomewhere/callofthehaunted.png")
            };

            _fileSystemService.Download(Arg.Any<Uri>(), Arg.Any<string>()).Returns(new DownloadedFile {ContentType = "image/png"});
            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).Rename(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public async Task Given_A_Valid_DownloadImageCommand_Should_Execute_Download_Exists_Once()
        {
            // Arrange
            var command = new DownloadImageCommand
            {
                ImageFolderPath = @"c:\windows",
                ImageFileName = "Call Of The Haunted",
                RemoteImageUrl = new Uri("http://filesomewhere/callofthehaunted.png")
            };

            _fileSystemService.Download(Arg.Any<Uri>(), Arg.Any<string>()).Returns(new DownloadedFile {ContentType = "image/png"});
            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).Exists(Arg.Any<string>());
        }
    }
}