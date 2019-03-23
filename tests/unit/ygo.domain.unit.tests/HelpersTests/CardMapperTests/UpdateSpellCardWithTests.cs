using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ygo.core.Models;
using ygo.core.Models.Db;
using ygo.domain.Mappers;
using ygo.tests.core;

namespace ygo.domain.unit.tests.HelpersTests.CardMapperTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class UpdateSpellCardWithTests
    {
        [Test]
        public void Given_A_SpellCard_When_Updated_With_CardModel_Then_CardSubCategory_Should_Have_A_Count_Of_2()
        {
            // Arrange
            var monsterCard = new Card();

            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int> { 4, 23 },
                TypeIds = new List<int>(),
                LinkArrowIds = new List<int>()
            };

            // Act
            CardMapper.UpdateSpellCardWith(monsterCard, cardModel);

            // Assert
            monsterCard.CardSubCategory.Should().HaveCount(2);
        }

        [Test]
        public void Given_A_SpellCard_When_Updated_With_CardModel_Then_CardSubCategory_Should_Contain_All_SubCategory_Ids()
        {
            // Arrange
            var expected = new[] { 4, 23 };

            var monsterCard = new Card();

            var cardModel = new CardModel
            {
                SubCategoryIds = new List<int> { 4, 23 },
                TypeIds = new List<int>(),
                LinkArrowIds = new List<int>()
            };

            // Act
            CardMapper.UpdateSpellCardWith(monsterCard, cardModel);

            // Assert
            monsterCard.CardSubCategory.Select(cs => cs.SubCategoryId).Should().ContainInOrder(expected);
        }
    }
}