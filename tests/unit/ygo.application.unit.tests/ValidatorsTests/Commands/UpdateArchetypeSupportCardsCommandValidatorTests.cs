using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo.application.Commands.UpdateArchetypeSupportCards;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestFixture]
    public class UpdateArchetypeSupportCardsCommandValidatorTests
    {
        private UpdateArchetypeSupportCardsCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UpdateArchetypeSupportCardsCommandValidator();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_ArchetypeId_Should_Fail_Validation(long archetypeId)
        {
            // Arrange
            var inputModel = new UpdateArchetypeSupportCardsCommand {ArchetypeId = archetypeId};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.ArchetypeId, inputModel);

            // Assert
            act.Invoke();
        }
    }
}