using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ygo.core.Models;
using ygo.domain.Mappers;
using ygo.tests.core;

namespace ygo.domain.unit.tests.HelpersTests.CardMapperTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class MapToMonsterCardTests
    {
        [Test]
        public void Given_A_CardModel_When_Mapped_To_Card_Then_CardSubCategory_Should_Have_A_Count_Of_2()
        {
            // Arrange
            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int> { 4, 23},
                TypeIds = new List<int>(),
                LinkArrowIds = new List<int>()
            };

            // Act
            var result = CardMapper.MapToMonsterCard(cardModel);

            // Assert
            result.CardSubCategory.Should().HaveCount(2);
        }

        [Test]
        public void Given_A_CardModel_When_Mapped_To_Card_Then_CardSubCategory_Should_Contain_All_SubCategory_Ids()
        {
            // Arrange
            var expected = new[]{ 4, 23 };

            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int> { 4, 23},
                TypeIds = new List<int>(),
                LinkArrowIds = new List<int>()
            };

            // Act
            var result = CardMapper.MapToMonsterCard(cardModel);

            // Assert
            result.CardSubCategory.Select(cs => cs.SubCategoryId).Should().ContainInOrder(expected);
        }


        [Test]
        public void Given_A_CardModel_When_Mapped_To_Card_Then_CardType_Should_Have_A_Count_Of_2()
        {
            // Arrange
            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int>(),
                TypeIds = new List<int> { 4, 23 },
                LinkArrowIds = new List<int>()
            };

            // Act
            var result = CardMapper.MapToMonsterCard(cardModel);

            // Assert
            result.CardType.Should().HaveCount(2);
        }

        [Test]
        public void Given_A_CardModel_When_Mapped_To_Card_Then_CardType_Should_Contain_All_Type_Ids()
        {
            // Arrange
            var expected = new[] { 4, 23 };

            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int>(),
                TypeIds = new List<int> { 4, 23 },
                LinkArrowIds = new List<int>()
            };

            // Act
            var result = CardMapper.MapToMonsterCard(cardModel);

            // Assert
            result.CardType.Select(cs => cs.TypeId).Should().ContainInOrder(expected);
        }

        [Test]
        public void Given_A_CardModel_When_Mapped_To_Card_Then_CardLinkArrow_Should_Have_A_Count_Of_2()
        {
            // Arrange
            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int>(),
                TypeIds = new List<int>(),
                LinkArrowIds = new List<int> { 4, 23 }
            };

            // Act
            var result = CardMapper.MapToMonsterCard(cardModel);

            // Assert
            result.CardLinkArrow.Should().HaveCount(2);
        }

        [Test]
        public void Given_A_CardModel_When_Mapped_To_Card_Then_CardLinkArrow_Should_Contain_All_Type_Ids()
        {
            // Arrange
            var expected = new[] { 4, 23 };

            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int>(),
                TypeIds = new List<int>(),
                LinkArrowIds = new List<int> { 4, 23 }
            };

            // Act
            var result = CardMapper.MapToMonsterCard(cardModel);

            // Assert
            result.CardLinkArrow.Select(cs => cs.LinkArrowId).Should().ContainInOrder(expected);
        }
    }
}