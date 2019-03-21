using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Commands;
using ygo.application.Commands.DownloadImage;
using ygo.application.Commands.UpdateArchetype;
using ygo.application.Configuration;
using ygo.application.Mappings.Profiles;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateArchetypeCommandHandlerTests
    {
        private IMediator _mediator;
        private IArchetypeService _archetypeService;
        private IOptions<ApplicationSettings> _settings;
        private UpdateArchetypeCommandHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _archetypeService = Substitute.For<IArchetypeService>();
            _settings = Substitute.For<IOptions<ApplicationSettings>>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new ArchetypeProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new UpdateArchetypeCommandHandler
            (
                _mediator,
                new UpdateArchetypeCommandValidator(),
                _archetypeService,
                _settings,
                mapper
            );
        }

        [Test]
        public async Task Given_An_Invalid_UpdateArchetypeCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateArchetypeCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateArchetypeCommand_Validation_Should_Return_Errors_List()
        {
            // Arrange
            var command = new UpdateArchetypeCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }


        [Test]
        public async Task Given_An_Valid_UpdateArchetypeCommand_And_Archetype_Is_Not_Found_Should_Return_Error_List()
        {
            // Arrange
            var command = new UpdateArchetypeCommand
            {
                Id = 23424,
                Name = "Toons",
                ProfileUrl = "http://www.toons.com"
            };

            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns((Archetype) null);

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Valid_UpdateArchetypeCommand_If_ImageUrl_Is_Not_Set_Should_Not_Invoke_DownloadImageCommand()
        {
            // Arrange
            var command = new UpdateArchetypeCommand
            {
                Id = 23424,
                Name = "Toons",
                ProfileUrl = "http://www.toons.com"
            };

            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _mediator.DidNotReceive().Send(Arg.Any<DownloadImageCommand>(), CancellationToken.None);
        }

        [Test]
        public async Task Given_An_Valid_UpdateArchetypeCommand_If_ImageUrl_Is_Set_Should_Invoke_DownloadImageCommand()
        {
            // Arrange
            var command = new UpdateArchetypeCommand
            {
                Id = 23424,
                Name = "Toons",
                ProfileUrl = "http://www.toons.com",
                ImageUrl = "http://www.toons.com/profile.png"
            };

            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());
            _mediator.Send(Arg.Any<DownloadImageCommand>(), CancellationToken.None).Returns(new CommandResult());
            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\windows"
            });

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<DownloadImageCommand>(), CancellationToken.None);
        }

        [Test]
        public async Task Given_An_Valid_UpdateArchetypeCommand_Should_Invoke_ArchetypeById_Once()
        {
            // Arrange
            var command = new UpdateArchetypeCommand
            {
                Id = 23424,
                Name = "Toons",
                ProfileUrl = "http://www.toons.com",
                ImageUrl = "http://www.toons.com/profile.png"
            };

            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());
            _mediator.Send(Arg.Any<DownloadImageCommand>(), CancellationToken.None).Returns(new CommandResult());
            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\windows"
            });

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _archetypeService.Received(1).ArchetypeById(Arg.Any<long>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateArchetypeCommand_Should_Invoke_Update_Once()
        {
            // Arrange
            var command = new UpdateArchetypeCommand
            {
                Id = 23424,
                Name = "Toons",
                ProfileUrl = "http://www.toons.com",
                ImageUrl = "http://www.toons.com/profile.png"
            };

            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());
            _mediator.Send(Arg.Any<DownloadImageCommand>(), CancellationToken.None).Returns(new CommandResult());
            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\windows"
            });

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _archetypeService.Received(1).Update(Arg.Any<Archetype>());
        }


        [Test]
        public async Task Given_An_Valid_UpdateArchetypeCommand_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateArchetypeCommand
            {
                Id = 23424,
                Name = "Toons",
                ProfileUrl = "http://www.toons.com",
                ImageUrl = "http://www.toons.com/profile.png"
            };

            _archetypeService.ArchetypeById(Arg.Any<long>()).Returns(new Archetype());
            _mediator.Send(Arg.Any<DownloadImageCommand>(), CancellationToken.None).Returns(new CommandResult());
            _settings.Value.Returns(new ApplicationSettings
            {
                ArchetypeImageFolderPath = @"c:\windows"
            });

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}