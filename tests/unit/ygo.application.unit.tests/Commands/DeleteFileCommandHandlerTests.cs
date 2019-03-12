using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.DeleteFile;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class DeleteFileCommandHandlerTests
    {
        private DeleteFileCommandHandler _sut;
        private IFileSystemService _fileSystemService;

        [SetUp]
        public void SetUp()
        {
            _fileSystemService = Substitute.For<IFileSystemService>();
            _sut = new DeleteFileCommandHandler(_fileSystemService);
        }

        [Test]
        public async Task Given_An_Invalid_LocalFileNameFullPath_DeleteFileCommand_Should_Fail()
        {
            // Arrange
            var command = new DeleteFileCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_LocalFileNameFullPath_DeleteFileCommand_Should_Return_Errors()
        {
            // Arrange
            var command = new DeleteFileCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Valid_LocalFileNameFullPath_DeleteFileCommand_Should_Complete_Succefully()
        {
            // Arrange
            var command = new DeleteFileCommand
            {
                LocalFileNameFullPath = @"c:\card\images\234.gif"
            };

            _fileSystemService.Delete(Arg.Any<string>());

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_An_Valid_LocalFileNameFullPath_Delete_Method_Should_Invoked_Once()
        {
            // Arrange
            var command = new DeleteFileCommand
            {
                LocalFileNameFullPath = @"c:\card\images\234.gif"
            };

            _fileSystemService.Delete(Arg.Any<string>());

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).Delete(Arg.Any<string>());
        }
    }
}