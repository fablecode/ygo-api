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

        [SetUp]
        public void Setup()
        {
            var fileSystemService = Substitute.For<IFileSystemService>();
            _settings = Substitute.For<IOptions<ApplicationSettings>>();

            _sut = new CardImageByNameQueryHandler(fileSystemService, _settings);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Given_An_Invalid_Card_Name_ISuccessful_Should_Be_False(string cardName)
        {
            // Arrange
            var query = new CardImageByNameQuery { Name = cardName };

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_CardImage_DirectoryPath_ISuccessful_Should_Be_False()
        {
            // Arrange
            var query = new CardImageByNameQuery { Name = "kuriboh"};

            _settings.Value.Returns(new ApplicationSettings { CardImageFolderPath = "D:SomeWeirdDirectory" });

            // Act
            var result =  await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }
    }
}