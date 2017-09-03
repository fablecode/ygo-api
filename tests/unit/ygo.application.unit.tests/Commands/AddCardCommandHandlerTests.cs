using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands;
using ygo.application.Commands.AddCard;
using ygo.domain.Validation;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class AddCardCommandHandlerTests
    {
        private AddCardCommandHandler _sut;
        private IMediator _mediator;

        [TestInitialize]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new AddCardCommandHandler(_mediator, new AddCardCommandValidator());
        }

        // Negative Tests
        [TestMethod]
        public async Task Given_An_Invalid_AddCardCommand_The_Command_Execution_Should_Return_A_List_Of_Errors()
        {
            // Arrange
            var command = new AddCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }


        // Positive Tests
    }

    public class AddMonsterCardCommandValidator : AbstractValidator<AddMonsterCardCommand>
    {
        public AddMonsterCardCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .CardNameValidator();
        }
    }

    public class AddSpellCardCommandValidator : AbstractValidator<AddSpellCardCommand>
    {
        public AddSpellCardCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .CardNameValidator();
        }
    }

    public class AddTrapCardCommandValidator : AbstractValidator<AddTrapCardCommand>
    {
        public AddTrapCardCommandValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .CardNameValidator();
        }
    }

    public class AddMonsterCardCommand : IRequest<CommandResult>
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public int AttributeId { get; set; }
        public List<int> SubCategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> LinkArrowIds { get; set; }
    }

    public class AddTrapCardCommand : IRequest<CommandResult>
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> SubCategoryIds { get; set; }
    }

    public class AddSpellCardCommand : IRequest<CommandResult>
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> SubCategoryIds { get; set; }
    }
}