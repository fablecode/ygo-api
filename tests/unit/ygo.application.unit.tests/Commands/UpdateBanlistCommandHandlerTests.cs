using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Commands.UpdateBanlist;
using ygo.application.Mappings.Profiles;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateBanlistCommandHandlerTests
    {
        private IBanlistService _banlistService;
        private UpdateBanlistCommandHandler _sut;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new BanlistProfile()); }
            );

            var mapper = config.CreateMapper();

            _banlistService = Substitute.For<IBanlistService>();
            _sut = new UpdateBanlistCommandHandler(_banlistService, new UpdateBanlistCommandValidator(), mapper);
        }

        [Test]
        public async Task Given_An_Invalid_UpdateBanlistCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateBanlistCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateBanlistCommand_Validation_Should_Error_List()
        {
            // Arrange
            var command = new UpdateBanlistCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Valid_UpdateBanlistCommand_If_Banlist_Is_Not_Found_Should_Return_Error_List()
        {
            // Arrange
            var command = new UpdateBanlistCommand
            {
                Id = 234234
            };

            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(null as Banlist);

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }


        [Test]
        public async Task Given_An_Valid_UpdateBanlistCommand_If_Banlist_Is_Not_Found_Should_Not_Execute_Update_Method()
        {
            // Arrange
            var command = new UpdateBanlistCommand
            {
                Id = 234234
            };

            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(null as Banlist);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistService.DidNotReceive().Update(Arg.Any<Banlist>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateBanlistCommand_If_Banlist_Is_Found_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateBanlistCommand
            {
                Id = 234234,
                FormatId = 2090,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.Now
            };

            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(new Banlist
            {
                Id = 2342,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.UtcNow
            });

            _banlistService.Update(Arg.Any<Banlist>()).Returns(new Banlist
            {
                Id = 2342,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.UtcNow
            });

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }


        [Test]
        public async Task Given_An_Valid_UpdateBanlistCommand_If_Banlist_Is_Found_Should_Execute_Update_Method_Once()
        {
            // Arrange
            var command = new UpdateBanlistCommand
            {
                Id = 234234,
                FormatId = 2090,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.Now
            };

            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(new Banlist
            {
                Id = 2342,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.UtcNow
            });

            _banlistService.Update(Arg.Any<Banlist>()).Returns(new Banlist
            {
                Id = 2342,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.UtcNow
            });

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistService.Received(1).Update(Arg.Any<Banlist>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateBanlistCommand_Should_Execute_GetBanlistById_Method_Once()
        {
            // Arrange
            var command = new UpdateBanlistCommand
            {
                Id = 234234,
                FormatId = 2090,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.Now
            };

            _banlistService.GetBanlistById(Arg.Any<long>()).Returns(new Banlist
            {
                Id = 2342,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.UtcNow
            });

            _banlistService.Update(Arg.Any<Banlist>()).Returns(new Banlist
            {
                Id = 2342,
                Name = "TCG January 1, 2019",
                ReleaseDate = DateTime.UtcNow
            });

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistService.Received(1).GetBanlistById(Arg.Any<long>());
        }
    }
}