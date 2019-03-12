using System;
using System.Threading;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.DownloadImage;
using ygo.application.Enums;
using ygo.application.Mappings.Profiles;
using ygo.application.Models.Cards.Input;
using ygo.application.Validations.Cards;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddCardCommandHandlerTests
    {
        private AddCardCommandHandler _sut;
        private IMediator _mediator;
        private ICardService _cardService;
        private IOptions<ApplicationSettings> _settings;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new CardProfile()); }
            );

            var mapper = config.CreateMapper();

            _mediator = Substitute.For<IMediator>();
            _settings = Substitute.For<IOptions<ApplicationSettings>>();

            _cardService = Substitute.For<ICardService>();
            _sut = new AddCardCommandHandler(_mediator, new CardValidator(), _cardService, _settings, mapper);
        }

        [Test]
        public async Task Given_An_Invalid_Card_AddCardCommand_Should_Not_Be_Successful()
        {
            // Arrange
            var command = new AddCardCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_Card_AddCardCommand_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new AddCardCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_Card_And_Validation_Fails_Should_Return_Error_List()
        {
            // Arrange
            var command = new AddCardCommand
            {
                Card = new CardInputModel()
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_A_Valid_Card_And_AddCardCommand_Fails_Should_Return_Error_List()
        {
            // Arrange
            var command = new AddCardCommand
            {
                Card = new CardInputModel
                {
                    CardType = YgoCardType.Spell,
                    Name = "Monster Reborn",
                    Description = "Special Summon a monster from any graveyard"
                }
            };

            _cardService.Add(Arg.Any<CardModel>()).Returns((Card) null);

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_A_Valid_Card_With_An_ImageUrl_Should_Invoke_DownloadImageCommand()
        {
            // Arrange
            var command = new AddCardCommand
            {
                Card = new CardInputModel
                {
                    CardType = YgoCardType.Spell,
                    Name = "Monster Reborn",
                    Description = "Special Summon a monster from any graveyard",
                    ImageUrl = new Uri("http://cardimageurl.com/card/image.jpg")
                }
            };

            _cardService.Add(Arg.Any<CardModel>()).Returns(new Card{ Id = 23424 });
            _settings.Value.Returns(new ApplicationSettings
            {
                CardImageFolderPath = "C:/cards/images"
            });

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<DownloadImageCommand>(), Arg.Any<CancellationToken>());
        }


        [Test]
        public async Task Given_A_Valid_Card_Should_Invoke_CardService_Add_Method_Once()
        {
            // Arrange
            var command = new AddCardCommand
            {
                Card = new CardInputModel
                {
                    CardType = YgoCardType.Spell,
                    Name = "Monster Reborn",
                    Description = "Special Summon a monster from any graveyard",
                    ImageUrl = new Uri("http://cardimageurl.com/card/image.jpg")
                }
            };

            _cardService.Add(Arg.Any<CardModel>()).Returns(new Card{ Id = 23424 });
            _settings.Value.Returns(new ApplicationSettings
            {
                CardImageFolderPath = "C:/cards/images"
            });


            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardService.Received(1).Add(Arg.Any<CardModel>());
        }

        [Test]
        public async Task Given_A_Valid_Card_ISuccessful_Should_Be_True()
        {
            // Arrange
            var command = new AddCardCommand
            {
                Card = new CardInputModel
                {
                    CardType = YgoCardType.Spell,
                    Name = "Monster Reborn",
                    Description = "Special Summon a monster from any graveyard",
                    ImageUrl = new Uri("http://cardimageurl.com/card/image.jpg")
                }
            };

            _cardService.Add(Arg.Any<CardModel>()).Returns(new Card{ Id = 23424 });
            _settings.Value.Returns(new ApplicationSettings
            {
                CardImageFolderPath = "C:/cards/images"
            });


            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}