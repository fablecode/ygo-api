using NSubstitute;
using NUnit.Framework;
using ygo.domain.Services;
using ygo.domain.SystemIO;
using ygo.tests.core;

namespace ygo.domain.unit.tests.ServiceTests.FileSystemServiceTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class DeleteTests
    {
        private IFileSystem _fileSystem;
        private FileSystemService _sut;

        [SetUp]
        public void SetUp()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _sut = new FileSystemService(_fileSystem);
        }

        [Test]
        public void Given_A_LocalFileFullPath_Should_Invoke_Delete_Method_Once()
        {
            // Arrange
            const string localFileFullPath = @"c:\images\pic.png";

            // Act
            _sut.Delete(localFileFullPath);

            // Assert
            _fileSystem.Received(1).Delete(Arg.Is(localFileFullPath));
        }

    }
}