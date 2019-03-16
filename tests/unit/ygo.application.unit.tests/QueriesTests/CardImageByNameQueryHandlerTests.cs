using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Queries.CardImageByName;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardImageByNameQueryHandlerTests
    {
        private CardImageByNameQueryHandler _sut;
        private IOptions<ApplicationSettings> _settings;
        private IFileSystemService _fileSystemService;

        [SetUp]
        public void Setup()
        {
            _fileSystemService = Substitute.For<IFileSystemService>();
            _settings = Substitute.For<IOptions<ApplicationSettings>>();

            _sut = new CardImageByNameQueryHandler(_fileSystemService, _settings);
        }

        [Test]
        public async Task Given_An_Invalid_CardImageByNameQuery_Should_Not_Execute_Successfully()
        {
            // Arrange
            _settings.Value.Returns(new ApplicationSettings
            {
                CardImageFolderPath = @"c:\images\cards"
            });

            // Act
            var result = await _sut.Handle(new CardImageByNameQuery(), CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Valid_CardImageByNameQuery_Should_Execute_Successfully()
        {
            // Arrange
            var query = new CardImageByNameQuery
            {
                Name = "kuriboh"
            };

            _settings.Value.Returns(new ApplicationSettings
            {
                CardImageFolderPath = @"c:\images\cards"
            });

            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);
            _fileSystemService.GetFiles(Arg.Any<string>(), Arg.Any<string>()).Returns(new[] { @"c:\images\cards\kuriboh.png" });

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_An_Valid_CardImageByQuery_Should_Execute_Exists_Method_Once()
        {
            // Arrange
            var query = new CardImageByNameQuery
            {
                Name = "kuriboh"
            };

            _settings.Value.Returns(new ApplicationSettings
            {
                CardImageFolderPath = @"c:\images\cards"
            });

            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);
            _fileSystemService.GetFiles(Arg.Any<string>(), Arg.Any<string>()).Returns(new[] { @"c:\images\cards\kuriboh.png" });

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).Exists(Arg.Any<string>());
        }

        [Test]
        public async Task Given_An_Valid_CardImageByQuery_Should_Execute_GetFiles_Method_Once()
        {
            // Arrange
            var query = new CardImageByNameQuery
            {
                Name = "kuriboh"
            };

            _settings.Value.Returns(new ApplicationSettings
            {
                CardImageFolderPath = @"c:\images\cards"
            });

            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);
            _fileSystemService.GetFiles(Arg.Any<string>(), Arg.Any<string>()).Returns(new[] { @"c:\images\cards\kuriboh.png" });

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).GetFiles(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}