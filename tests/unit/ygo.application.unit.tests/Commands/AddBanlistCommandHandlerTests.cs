using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.AddBanlist;
using ygo.application.Mappings.Profiles;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class AddBanlistCommandHandlerTests
    {
        private AddBanlistCommandHandler _sut;
        private IBanlistService _banlistService;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new BanlistProfile()); }
            );

            var mapper = config.CreateMapper();

            _banlistService = Substitute.For<IBanlistService>();
            _sut = new AddBanlistCommandHandler
            (
               _banlistService,
               new AddBanlistCommandValidator(),
               mapper
            );
        }

        [Test]
        public async Task Given_An_Invalid_Banlist_AddBanlist_Command_Should_Not_Be_Successful()
        {
            // Arrange
            var command = new AddBanlistCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_Banlist_AddBanlist_Command_Should_Return_A_List_Errors()
        {
            // Arrange
            var command = new AddBanlistCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_Banlist_AddBanlist_Command_Should_Not_Invoke_BanlistService_Add_Method()
        {
            // Arrange
            var command = new AddBanlistCommand();

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistService.DidNotReceive().Add(Arg.Any<Banlist>());
        }

        [Test]
        public async Task Given_An_Valid_Banlist_Should_Invoke_BanlistService_Add_Once()
        {
            // Arrange
            var command = new AddBanlistCommand
            {
                Id = 2342,
                FormatId = 32,
                Name = "January 2019",
                ReleaseDate = DateTime.Now
            };

            _banlistService.Add(Arg.Any<Banlist>()).Returns(new Banlist { Id = 1231});

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _banlistService.Received(1).Add(Arg.Any<Banlist>());
        }
    }
}