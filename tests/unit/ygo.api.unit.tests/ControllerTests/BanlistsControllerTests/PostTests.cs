using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ygo.api.Controllers;
using ygo.application.Commands;
using ygo.application.Commands.AddBanlist;
using ygo.application.Dto;
using ygo.application.Queries.BanlistById;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.BanlistsControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class PostTests
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
        public async Task Given_An_AddBanlistCommand_If_Banlist_Already_Exists_Should_Return_StatusCode()
        {
            // Arrange
            var query = new AddBanlistCommand();

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns(new BanlistDto());

            // Act
            var result = await _sut.Post(query);

            // Assert
            result.Should().BeOfType<ObjectResult>();
        }

        [Test]
        public async Task Given_An_AddBanlistCommand_If_Banlist_Already_Exists_Should_Return_StatusCode_With_HttpStatusCode_Conflict()
        {
            // Arrange
            var query = new AddBanlistCommand();

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns(new BanlistDto());

            // Act
            var result = await _sut.Post(query) as StatusCodeResult;

            // Assert
            result?.StatusCode.Should().Be((int)HttpStatusCode.Conflict);
        }


        [Test]
        public async Task Given_An_AddBanlistCommand_If_Banlist_Does_Not_Exists_But_Fails_Validation_Should_Return_BadRequest()
        {
            // Arrange
            var query = new AddBanlistCommand();

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns((BanlistDto)null);

            _mediator.Send(Arg.Any<AddBanlistCommand>()).Returns(new CommandResult { Errors = new List<string> { "Name must not be empty." } });

            // Act
            var result = await _sut.Post(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Test]
        public async Task Given_An_AddBanlistCommand_If_Banlist_Does_Not_Exists_But_Fails_Validation_Should_Return_BadRequest_With_Errors()
        {
            // Arrange
            const string expected = "Name must not be empty.";

            var query = new AddBanlistCommand();

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns((BanlistDto)null);

            _mediator.Send(Arg.Any<AddBanlistCommand>()).Returns(new CommandResult { Errors = new List<string> { "Name must not be empty." } });

            // Act
            var result = await _sut.Post(query) as BadRequestObjectResult;

            // Assert
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_An_AddBanlistCommand_If_Banlist_Does_Not_Exists_And_Passes_Validation_Should_Return_CreatedAtRouteResult()
        {
            // Arrange
            var query = new AddBanlistCommand();

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns((BanlistDto)null);

            _mediator.Send(Arg.Any<AddBanlistCommand>()).Returns(new CommandResult { IsSuccessful = true, Data = 23423 });

            // Act
            var result = await _sut.Post(query);

            // Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Test]
        public async Task Given_An_AddBanlistCommand_If_Banlist_Does_Not_Exists_And_Passes_Validation_Should_Invoke_ArchetypeByIdQuery_Once()
        {
            // Arrange
            var query = new AddBanlistCommand();

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns((BanlistDto)null);

            _mediator.Send(Arg.Any<AddBanlistCommand>()).Returns(new CommandResult { IsSuccessful = true, Data = 23423 });

            // Act
            await _sut.Post(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<BanlistByIdQuery>());
        }

        [Test]
        public async Task Given_An_AddBanlistCommand_If_Banlist_Does_Not_Exists_And_Passes_Validation_Should_Invoke_AddBanlistCommand_Once()
        {
            // Arrange
            var query = new AddBanlistCommand();

            _mediator.Send(Arg.Any<BanlistByIdQuery>()).Returns((BanlistDto)null);

            _mediator.Send(Arg.Any<AddBanlistCommand>()).Returns(new CommandResult { IsSuccessful = true, Data = 23423 });

            // Act
            await _sut.Post(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<AddBanlistCommand>());
        }
    }
}