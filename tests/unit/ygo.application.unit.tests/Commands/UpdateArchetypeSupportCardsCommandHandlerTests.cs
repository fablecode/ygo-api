using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Commands.UpdateArchetypeSupportCards;
using ygo.application.Mappings.Profiles;
using ygo.core.Services;

namespace ygo.application.unit.tests.Commands
{
    public class UpdateArchetypeSupportCardsCommandHandlerTests
    {
        private IArchetypeSupportCardsService _archetypeSupportCardsService;
        private UpdateArchetypeSupportCardsCommandHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _archetypeSupportCardsService = Substitute.For<IArchetypeSupportCardsService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new ArchetypeProfile()); }
            );

            var mapper = config.CreateMapper();



            _sut = new UpdateArchetypeSupportCardsCommandHandler(new UpdateArchetypeSupportCardsCommandValidator(), _archetypeSupportCardsService, mapper);
        }

        [Test]
        public async Task Given_An_Invalid_UpdateArchetypeSupportCardsCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateArchetypeSupportCardsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateArchetypeSupportCardsCommand_Validation_Should_Return_Error_List()
        {
            // Arrange
            var command = new UpdateArchetypeSupportCardsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }


        [Test]
        public async Task Given_An_Valid_ArchetypeSupportCards_With_No_Cards_Should_Not_Invoke_Update_Method()
        {
            // Arrange
            var command = new UpdateArchetypeSupportCardsCommand
            {
                Cards = new List<string>()
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _archetypeSupportCardsService.DidNotReceive().Update(Arg.Any<long>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public async Task Given_An_Valid_ArchetypeSupportCards_With_Cards_Should_Invoke_Update_Method_Once()
        {
            // Arrange
            var command = new UpdateArchetypeSupportCardsCommand
            {
                ArchetypeId = 23423,
                Cards = new List<string>
                {
                    "Monster Reborn",
                    "Call Of The Haunted"
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _archetypeSupportCardsService.Received(1).Update(Arg.Any<long>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public async Task Given_An_Valid_ArchetypeSupportCards_With_Cards_Command_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateArchetypeSupportCardsCommand
            {
                ArchetypeId = 23423,
                Cards = new List<string>
                {
                    "Monster Reborn",
                    "Call Of The Haunted"
                }
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Given_An_Valid_ArchetypeSupportCards_With_No_Cards_Command_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateArchetypeSupportCardsCommand
            {
                ArchetypeId = 2342,
                Cards = new List<string>()
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}