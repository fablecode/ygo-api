using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Commands.UpdateTips;
using ygo.application.Dto;
using ygo.core.Models.Db;
using ygo.core.Services;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateTipsCommandHandlerTests
    {
        private UpdateTipsCommandHandler _sut;
        private ICardTipService _cardTipService;

        [SetUp]
        public void SetUp()
        {
            _cardTipService = Substitute.For<ICardTipService>();
            _sut = new UpdateTipsCommandHandler(_cardTipService, new UpdateTipsCommandValidator());
        }

        [Test]
        public async Task Given_An_Invalid_UpdateTipsCommand_Validation_Should_Fail()
        {
            // Arrange
            var command = new UpdateTipsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Given_An_Invalid_UpdateTipsCommand_Validation_Should_Return_Error_List()
        {
            // Arrange
            var command = new UpdateTipsCommand();

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Given_An_Valid_UpdateTipsCommand_If_TipList_Is_Empty_Should_Not_Execute_Update_Method()
        {
            // Arrange
            var command = new UpdateTipsCommand
            {
                CardId = 34535,
                Tips = new List<TipSectionDto>()
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardTipService.DidNotReceive().Update(Arg.Any<List<TipSection>>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateTipsCommand_If_TipList_Is_Not_Empty_Should_Execute_Update_Method()
        {
            // Arrange
            var command = new UpdateTipsCommand
            {
                CardId = 34535,
                Tips = new List<TipSectionDto>
                {
                    new TipSectionDto
                    {
                        Name = "List of Monsters",
                        Tips = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardTipService.Received(1).Update(Arg.Any<List<TipSection>>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateTipsCommand_Should_Execute_DeleteByCardId_Method()
        {
            // Arrange
            var command = new UpdateTipsCommand
            {
                CardId = 34535,
                Tips = new List<TipSectionDto>
                {
                    new TipSectionDto
                    {
                        Name = "List of Monsters",
                        Tips = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _cardTipService.Received(1).DeleteByCardId(Arg.Any<long>());
        }

        [Test]
        public async Task Given_An_Valid_UpdateTipsCommand_Should_Execute_Successfully()
        {
            // Arrange
            var command = new UpdateTipsCommand
            {
                CardId = 34535,
                Tips = new List<TipSectionDto>
                {
                    new TipSectionDto
                    {
                        Name = "List of Monsters",
                        Tips = new List<string>{ "Sangan" }
                    }
                }
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}