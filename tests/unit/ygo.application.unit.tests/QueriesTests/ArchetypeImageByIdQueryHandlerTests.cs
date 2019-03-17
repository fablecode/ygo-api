using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Configuration;
using ygo.application.Queries.ArchetypeImageById;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class ArchetypeImageByIdQueryHandlerTests
    {
        private IFileSystemService _fileSystemService;
        private IOptions<ApplicationSettings> _settings;
        private ArchetypeImageByIdQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _fileSystemService = Substitute.For<IFileSystemService>();
            _settings = Substitute.For<IOptions<ApplicationSettings>>();

            _sut = new ArchetypeImageByIdQueryHandler(_fileSystemService, _settings);
        }

        [Test]
        public async Task Given_An_Invalid_ArchetypeImageByQuery_Should_Not_Execute_Successfully()
        {
            // Arrange
            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\images\archetypes"
            });

            // Act
            var result = await _sut.Handle(new ArchetypeImageByIdQuery(), CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Valid_ArchetypeImageByQuery_Should_Execute_Successfully()
        {
            // Arrange
            var query = new ArchetypeImageByIdQuery
            {
                Id = 2342342
            };

            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\images\archetypes"
            });

            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);
            _fileSystemService.GetFiles(Arg.Any<string>(), Arg.Any<string>()).Returns(new[] { @"c:\images\archetypes\2342342.png"});

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_An_Valid_ArchetypeImageByQuery_Should_Execute_Exists_Method_Once()
        {
            // Arrange
            var query = new ArchetypeImageByIdQuery
            {
                Id = 2342342
            };

            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\images\archetypes"
            });

            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);
            _fileSystemService.GetFiles(Arg.Any<string>(), Arg.Any<string>()).Returns(new[] { @"c:\images\archetypes\2342342.png"});

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).Exists(Arg.Any<string>());
        }

        [Test]
        public async Task Given_An_Valid_ArchetypeImageByQuery_Should_Execute_GetFiles_Method_Once()
        {
            // Arrange
            var query = new ArchetypeImageByIdQuery
            {
                Id = 2342342
            };

            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\images\archetypes"
            });

            _fileSystemService.Exists(Arg.Any<string>()).Returns(true);
            _fileSystemService.GetFiles(Arg.Any<string>(), Arg.Any<string>()).Returns(new[] { @"c:\images\archetypes\2342342.png"});

            // Act
            await _sut.Handle(query, CancellationToken.None);

            // Assert
            _fileSystemService.Received(1).GetFiles(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}