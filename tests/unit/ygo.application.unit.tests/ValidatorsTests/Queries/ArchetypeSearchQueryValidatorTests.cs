using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ygo.application.Queries.ArchetypeSearch;

namespace ygo.application.unit.tests.ValidatorsTests.Queries
{
    [TestFixture]
    public class ArchetypeSearchQueryValidatorTests
    {
        private ArchetypeSearchQueryValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ArchetypeSearchQueryValidator();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_PageNumber_Validation_Should_Fail(int pageNumber)
        {
            // Arrange
            var inputModel = new ArchetypeSearchQuery {PageNumber = pageNumber};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.PageNumber, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(11)]
        public void Given_An_Invalid_PageSize_Validation_Should_Fail(int pageSize)
        {
            // Arrange
            var inputModel = new ArchetypeSearchQuery {PageSize = pageSize};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(a => a.PageSize, inputModel);

            // Assert
            act.Invoke();
        }
    }
}