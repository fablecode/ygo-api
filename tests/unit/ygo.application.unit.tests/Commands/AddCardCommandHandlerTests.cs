using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ygo.application.Commands;
using ygo.application.Commands.AddCard;
using ygo.application.Repository;
using ygo.application.unit.tests.ValidatorsTests.Commands;

namespace ygo.application.unit.tests.Commands
{
    [TestClass]
    public class AddCardCommandHandlerTests
    {
        private AddCardCommandHandler _sut;

        [TestInitialize]
        public void Setup()
        {
            var repository = Substitute.For<ICardRepository>();
            IValidator<AddCardCommand> validator = new AddCardCommandValidator();

            _sut = new AddCardCommandHandler(repository, validator);
        }

        [TestMethod]
        public async Task Given_An_Invalid_AddCardCommand_The_Command_Execution_Should_Be_Not_Successful()
        {
            // Arrange
            var command = new AddCardCommand();

            // Act
            var result = await _sut.Handle(command);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

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


    }

    public class AddCardCommandHandler : IAsyncRequestHandler<AddCardCommand, CommandResult>
    {
        private readonly ICardRepository _repository;
        private readonly IValidator<AddCardCommand> _validator;

        public AddCardCommandHandler(ICardRepository repository, IValidator<AddCardCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public Task<CommandResult> Handle(AddCardCommand message)
        {
            var response = new CommandResult();


            var validationResults = _validator.Validate(message);
            if (validationResults.IsValid)
            {
                
            }

            return Task.FromResult(response);
        }
    }
}