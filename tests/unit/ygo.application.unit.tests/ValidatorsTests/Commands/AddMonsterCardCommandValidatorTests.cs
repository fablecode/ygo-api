using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ygo.application.Commands.AddMonsterCard;

namespace ygo.application.unit.tests.ValidatorsTests.Commands
{
    [TestClass]
    public class AddMonsterCardCommandValidatorTests
    {
        private AddMonsterCardCommandValidator _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new AddMonsterCardCommandValidator();
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void Given_A_MonsterCard_Where_CardNumber_Is_Invalid_Validation_Should_Fail(int cardNumber)
        {
            // Arrange
            var command = new AddMonsterCardCommand { CardNumber= cardNumber };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardNumber, command);

            // Assert
            act.Invoke();
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Given_An_Invalid_MonsterCardName_Validation_Should_Fail(string cardName)
        {
            // Arrange
            var command = new AddMonsterCardCommand { Name = cardName };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_MonsterCardName_If_Length_Is_Less_Than_2_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddMonsterCardCommand { Name = new string('c', 1) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_MonsterCardName_If_Length_Is_Greater_Than_255_Validation_Should_Fail()
        {
            // Arrange
            var command = new AddMonsterCardCommand { Name = new string('c', 256) };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Name, command);

            // Assert
            act.Invoke();
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(23)]
        public void Given_A_MonsterCard_Where_CardLevel_Is_Invalid_Validation_Should_Fail(int cardLevel)
        {
            // Arrange
            var command = new AddMonsterCardCommand { CardLevel = cardLevel };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardLevel, command);

            // Assert
            act.Invoke();
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(23)]
        public void Given_A_MonsterCard_Where_CardRank_Is_Invalid_Validation_Should_Fail(int cardRank)
        {
            // Arrange
            var command = new AddMonsterCardCommand { CardRank = cardRank };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.CardRank, command);

            // Assert
            act.Invoke();
        }

        [DataRow(-1)]
        [DataRow(20001)]
        public void Given_A_MonsterCard_Where_Atk_Is_Invalid_Validation_Should_Fail(int atk)
        {
            // Arrange
            var command = new AddMonsterCardCommand { Atk = atk };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Atk, command);

            // Assert
            act.Invoke();
        }

        [DataRow(-1)]
        [DataRow(20001)]
        public void Given_A_MonsterCard_Where_Def_Is_Invalid_Validation_Should_Fail(int def)
        {
            // Arrange
            var command = new AddMonsterCardCommand { Def = def };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.Def, command);

            // Assert
            act.Invoke();
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void Given_A_MonsterCard_Where_AttributeId_Is_Invalid_Validation_Should_Fail(int attributeId)
        {
            // Arrange
            var command = new AddMonsterCardCommand { AttributeId = attributeId };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.AttributeId, command);

            // Assert
            act.Invoke();
        }

        [TestMethod]
        public void Given_A_MonsterCard_Where_SubCategoryIds_Is_Invalid_Validation_Should_Fail()
        {
            // Arrange
            var values = new[]
            {
                new {subCategoryIds = default(List<int>)},
                new {subCategoryIds = new List<int>()}
            };


            values.ToList().ForEach(val =>
            {
                var command = new AddMonsterCardCommand { SubCategoryIds = val.subCategoryIds };

                // Act
                Action act = () => _sut.ShouldHaveValidationErrorFor(c => c.SubCategoryIds, command);

                // Assert
                act.Invoke();
            });
        }

        [TestMethod]
        public void Given_A_Valid_MonsterCard_Validation_Should_Pass()
        {
            // Arrange
            var monsterCard = new AddMonsterCardCommand
            {
                Name = "Blue-Eyes White Dragon",
                AttributeId = 5, // Light,
                CardLevel = 8,
                SubCategoryIds = new List<int>
                {
                    1 // Normal Monster
                },
                TypeIds = new List<int>
                {
                    8 // Dragon
                },
                Description = "This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.",
                Atk = 3000,
                Def = 2500,
                CardNumber = 89631139,
            };

            // Act
            var result = _sut.Validate(monsterCard);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}