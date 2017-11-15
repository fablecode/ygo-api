using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ygo.application.Queries.ArchetypeByName;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestClass]
    public class ArchetypeByNameQueryValidatorTests
    {
        private ArchetypeByNameQueryValidator _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new ArchetypeByNameQueryValidator();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Invalid_Archetype_Name_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var query = new ArchetypeByNameQuery { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(q => q.Name, query);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_An_Invalid_Archetype_Name_Validation_Should_StopOnFirstFailure()
        {
            // Arrange

            // Act
            var result = _sut.Validate(new ArchetypeByNameQuery());

            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod]
        public void Given_An_Valid_Card_Name_Validation_Should_Pass()
        {
            // Arrange
            var query = new ArchetypeByNameQuery { Name = "GoodArchetypeName" };

            // Act
            var result = _sut.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}