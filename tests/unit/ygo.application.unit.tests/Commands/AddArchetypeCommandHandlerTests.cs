using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Commands.AddArchetype;
using ygo.application.Commands.DownloadImage;
using ygo.application.Configuration;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddArchetypeCommandHandlerTests
    {
        private IMediator _mediator;
        private IArchetypeService _archetypeService;
        private IOptions<ApplicationSettings> _settings;
        private AddArchetypeCommandHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _archetypeService = Substitute.For<IArchetypeService>();
            _settings = Substitute.For<IOptions<ApplicationSettings>>();
            _sut = new AddArchetypeCommandHandler
            (
                _mediator, 
                new AddArchetypeCommandValidator(), 
                _archetypeService, 
                _settings
            );
        }

        [Test]
        public async Task Given_An_Invalid_Archetype_AddArchetype_Command_Should_Not_Be_Successful()
        {
            // Arrange
            var command = new AddArchetypeCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_Archetype_AddArchetype_Command_Should_Return_A_List_Errors()
        {
            // Arrange
            var command = new AddArchetypeCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Valid_Archetype_If_ImageUrl_Is_Not_Specified_Should_Not_Invoke_DownloadImage_Command()
        {
            // Arrange
            var command = new AddArchetypeCommand
            {
                Name = "archetype",
                ArchetypeNumber = 2342423,
                ProfileUrl = "http://archetypeprofileurl.com"
            };

            _archetypeService.Add(Arg.Any<Archetype>()).Returns(new Archetype());

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _mediator.DidNotReceive().Send(Arg.Any<DownloadImageCommand>());
        }

        [Test]
        public async Task Given_An_Valid_Archetype_If_ImageUrl_Is_Not_Specified_Should_Invoke_DownloadImage_Command_Once()
        {
            // Arrange
            var command = new AddArchetypeCommand
            {
                Name = "archetype",
                ArchetypeNumber = 2342423,
                ProfileUrl = "http://archetypeprofileurl.com",
                ImageUrl = "http://archetypeimageurl/image/3242"
            };

            _archetypeService.Add(Arg.Any<Archetype>()).Returns(new Archetype { Id = 234234 });
            _settings.Value.Returns(new ApplicationSettings { ArchetypeImageFolderPath = @"c:\images\archetypes\profile"});


            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<DownloadImageCommand>());
        }

        [Test]
        public async Task Given_An_Valid_Archetype_Should_Invoke_Add_Archetype_Method_Once()
        {
            // Arrange
            var command = new AddArchetypeCommand
            {
                Name = "archetype",
                ArchetypeNumber = 2342423,
                ProfileUrl = "http://archetypeprofileurl.com",
                ImageUrl = "http://archetypeimageurl/image/3242"
            };

            _archetypeService.Add(Arg.Any<Archetype>()).Returns(new Archetype { Id = 234234 });
            _settings.Value.Returns(new ApplicationSettings { ArchetypeImageFolderPath = @"c:\images\archetypes\profile"});


            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _archetypeService.Received(1).Add(Arg.Any<Archetype>());
        }
    }
}