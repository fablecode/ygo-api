﻿using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Queries.CardImageByName;
using ygo.domain.Service;

namespace ygo.application.unit.tests.QueriesTests
{
    [TestClass]
    public class CardImageByNameQueryHandlerTests
    {
        private CardImageByNameQueryHandler _sut;
        private IOptions<ApplicationSettings> _settings;

        [TestInitialize]
        public void Setup()
        {
            var fileSystemService = Substitute.For<IFileSystemService>();
            _settings = Substitute.For<IOptions<ApplicationSettings>>();

            _sut = new CardImageByNameQueryHandler(fileSystemService, _settings);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public async Task Given_An_Invalid_Card_Name_ISuccessful_Should_Be_False(string cardName)
        {
            // Arrange
            var query = new CardImageByNameQuery { Name = cardName };

            // Act
            var result = await _sut.Handle(query);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [TestMethod]
        public async Task Given_An_Invalid_CardImage_DirectoryPath_ISuccessful_Should_Be_False()
        {
            // Arrange
            var query = new CardImageByNameQuery { Name = "kuriboh"};

            _settings.Value.Returns(new ApplicationSettings { CardImageFolderPath = "D:SomeWierdDirectory" });

            // Act
            var result =  await _sut.Handle(query);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }
    }
}