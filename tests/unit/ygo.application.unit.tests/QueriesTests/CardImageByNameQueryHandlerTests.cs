using System.Threading;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using NUnit.Framework;
using ygo.application.Queries.CardImageByName;
using ygo.domain.Service;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestFixture]
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

            _settings.Value.Returns(new ApplicationSettings { CardImageFolderPath = "D:SomeWierdDirectory" });

            // Act
            var result =  await _sut.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }
    }
}