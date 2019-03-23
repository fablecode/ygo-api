using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ygo.application.Commands.UpdateArchetypeCards;
using ygo.application.Mappings.Profiles;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateArchetypeCardsCommandHandlerTests
    {
        private IArchetypeCardsService _archetypeCardsService;
        private UpdateArchetypeCardsCommandHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _archetypeCardsService = Substitute.For<IArchetypeCardsService>();

            var config = new MapperConfiguration
            (
                cfg => { cfg.AddProfile(new ArchetypeProfile()); }
            );

            var mapper = config.CreateMapper();


            _sut = new UpdateArchetypeCardsCommandHandler(new UpdateArchetypeCardsCommandValidator(), _archetypeCardsService, mapper);
        }

        [Test]
        public async Task Given_An_Invalid_UpdateArchetypeCardsCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateArchetypeCardsCommand_Validation_Should_Return_Error_List()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }


        [Test]
        public async Task Given_An_Valid_Archetype_With_No_Cards_Should_Not_Invoke_Update_Method()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand
            {
                Cards = new List<string>()
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _archetypeCardsService.DidNotReceive().Update(Arg.Any<long>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public async Task Given_An_Valid_Archetype_With_Cards_Should_Invoke_Update_Method_Once()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand
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
            await _archetypeCardsService.Received(1).Update(Arg.Any<long>(), Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public async Task Given_An_Valid_Archetype_With_Cards_Command_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand
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
        public async Task Given_An_Valid_Archetype_With_No_Cards_Command_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateArchetypeCardsCommand
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