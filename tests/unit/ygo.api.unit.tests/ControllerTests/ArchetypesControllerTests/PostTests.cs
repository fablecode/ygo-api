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
using ygo.application.Commands.AddArchetype;
using ygo.application.Dto;
using ygo.application.Queries.ArchetypeById;
using ygo.tests.core;

namespace ygo.api.unit.tests.ControllerTests.ArchetypesControllerTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class PostTests
    {
        private IMediator _mediator;
        private ArchetypesController _sut;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new ArchetypesController(_mediator);
        }

        [Test]
        public async Task Given_An_AddArchetypeCommand_If_Archetype_Already_Exists_Should_Return_StatusCode()
        {
            // Arrange
            var query = new AddArchetypeCommand();

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns(new ArchetypeDto());

            // Act
            var result = await _sut.Post(query);

            // Assert
            result.Should().BeOfType<StatusCodeResult>();
        }

        [Test]
        public async Task Given_An_AddArchetypeCommand_If_Archetype_Already_Exists_Should_Return_StatusCode_With_HttpStatusCode_Conflict()
        {
            // Arrange
            var query = new AddArchetypeCommand();

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns(new ArchetypeDto());

            // Act
            var result = await _sut.Post(query) as StatusCodeResult;

            // Assert
            result?.StatusCode.Should().Be((int) HttpStatusCode.Conflict);
        }


        [Test]
        public async Task Given_An_AddArchetypeCommand_If_Archetype_Does_Not_Exists_But_Fails_Validation_Should_Return_BadRequest()
        {
            // Arrange
            var query = new AddArchetypeCommand();

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns((ArchetypeDto) null);

            _mediator.Send(Arg.Any<AddArchetypeCommand>()).Returns(new CommandResult { Errors = new List<string>{ "Name must not be empty." } });

            // Act
            var result = await _sut.Post(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Test]
        public async Task Given_An_AddArchetypeCommand_If_Archetype_Does_Not_Exists_But_Fails_Validation_Should_Return_BadRequest_With_Errors()
        {
            // Arrange
            const string expected = "Name must not be empty.";

            var query = new AddArchetypeCommand();

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns((ArchetypeDto) null);

            _mediator.Send(Arg.Any<AddArchetypeCommand>()).Returns(new CommandResult { Errors = new List<string>{ "Name must not be empty." } });

            // Act
            var result = await _sut.Post(query) as BadRequestObjectResult;

            // Assert
            var errors = result?.Value as IEnumerable<string>;
            errors.Should().ContainSingle(expected);
        }

        [Test]
        public async Task Given_An_AddArchetypeCommand_If_Archetype_Does_Not_Exists_And_Passes_Validation_Should_Return_CreatedAtRouteResult()
        {
            // Arrange
            var query = new AddArchetypeCommand();

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns((ArchetypeDto)null);

            _mediator.Send(Arg.Any<AddArchetypeCommand>()).Returns(new CommandResult{ IsSuccessful = true, Data = 23423});

            // Act
            var result = await _sut.Post(query);

            // Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Test]
        public async Task Given_An_AddArchetypeCommand_If_Archetype_Does_Not_Exists_And_Passes_Validation_Should_Invoke_ArchetypeByIdQuery_Once()
        {
            // Arrange
            var query = new AddArchetypeCommand();

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns((ArchetypeDto)null);

            _mediator.Send(Arg.Any<AddArchetypeCommand>()).Returns(new CommandResult{ IsSuccessful = true, Data = 23423});

            // Act
            await _sut.Post(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<ArchetypeByIdQuery>());
        }

        [Test]
        public async Task Given_An_AddArchetypeCommand_If_Archetype_Does_Not_Exists_And_Passes_Validation_Should_Invoke_AddArchetypeCommand_Once()
        {
            // Arrange
            var query = new AddArchetypeCommand();

            _mediator.Send(Arg.Any<ArchetypeByIdQuery>()).Returns((ArchetypeDto)null);

            _mediator.Send(Arg.Any<AddArchetypeCommand>()).Returns(new CommandResult{ IsSuccessful = true, Data = 23423});

            // Act
            await _sut.Post(query);

            // Assert
            await _mediator.Received(1).Send(Arg.Any<AddArchetypeCommand>());
        }
    }
}