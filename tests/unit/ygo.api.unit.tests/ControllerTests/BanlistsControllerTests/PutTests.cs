using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.UpdateBanlist;
using ygo.application.Queries.BanlistExists;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.BanlistsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class PutTests
    {
        private IMediator _mediator;
        private BanlistsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new BanlistsController(_mediator);
        }

        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Banlist_Is_Not_Found_Should_Return_NotFoundResult()
        {
            // Arrange
            var query = new UpdateBanlistCommand();

            _mediator.Send(Arg.Any<BanlistExistsQuery>()).Returns(false);

            // Act
            var result = await _sut.Put(query);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Banlist_Is_Found_But_Fails_Validation_Should_Return_BadRequest()
        {
            // Arrange
            var query = new UpdateBanlistCommand();

            _mediator.Send(Arg.Any<BanlistExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateBanlistCommand>()).Returns(new CommandResult { Errors = new List<string> { "Name must not be empty." } });

            // Act
            var result = await _sut.Put(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Banlist_Is_Found_And_Passes_Validation_Should_Return_OkResult()
        {
            // Arrange
            var query = new UpdateBanlistCommand();

            _mediator.Send(Arg.Any<BanlistExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateBanlistCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            var result = await _sut.Put(query);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }


        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Banlist_Is_Found_And_Passes_Validation_Should_Invoke_BanlistExistsQuery_Once()
        {
            // Arrange
            var query = new UpdateBanlistCommand();

            _mediator.Send(Arg.Any<BanlistExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateBanlistCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            await _sut.Put(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<BanlistExistsQuery>());
        }

        [Test]
        public async Task Given_A_UpdateBanlistCommand_If_Banlist_Is_Found_And_Passes_Validation_Should_Invoke_UpdateBanlistCommand_Once()
        {
            // Arrange
            var query = new UpdateBanlistCommand();

            _mediator.Send(Arg.Any<BanlistExistsQuery>()).Returns(true);
            _mediator.Send(Arg.Any<UpdateBanlistCommand>()).Returns(new CommandResult { IsSuccessful = true });

            // Act
            await _sut.Put(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<UpdateBanlistCommand>());
        }
    }
}