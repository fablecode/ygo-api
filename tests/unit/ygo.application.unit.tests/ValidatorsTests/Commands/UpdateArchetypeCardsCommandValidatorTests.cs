using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ygo.application.Commands.UpdateArchetypeCards;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class UpdateArchetypeCardsCommandValidatorTests
    {
        private UpdateArchetypeCardsCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UpdateArchetypeCardsCommandValidator();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_ArchetypeId_Should_Fail_Validation(long archetypeId)
        {
            // Arrange
            var inputModel = new UpdateArchetypeCardsCommand {ArchetypeId = archetypeId};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.ArchetypeId, inputModel);

            // Assert
            act.Invoke();
        }
    }
}