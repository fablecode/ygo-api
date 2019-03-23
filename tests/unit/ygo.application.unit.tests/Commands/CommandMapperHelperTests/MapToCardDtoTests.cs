using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using ygo.application.Commands;
using ygo.application.Mappings.Profiles;
using ygo.core.Models.Db;
using ygo.tests.core;

namespace ygo.application.unit.tests.Commands.CommandMapperHelperTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class MapToCardDtoTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.AddProfile(new CardProfile());
                    cfg.AddProfile(new AttributeProfile());
                }
            );

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Given_An_Invalid_Card_Should_Return_Null()
        {
            // Arrange
            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, null);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void Given_A_Card_When_Mapped_To_CardDto_Then_Attribute_Should_Not_Be_Null()
        {
            // Arrange
            var card = new Card
            {
                Name = "Call Of The Haunted",
                CardAttribute = new List<CardAttribute>
                {
                    new CardAttribute
                    {
                        Attribute = new Attribute
                        {
                            Id = 4324,
                            Name = "Earth"
                        }
                    }
                }
            };

            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, card);

            // Assert
            result.Attribute.Should().NotBeNull();
        }

        [Test]
        public void Given_A_Card_When_Mapped_To_CardDto_Then_Link_Should_Equal_2()
        {
            // Arrange
            var card = new Card
            {
                Name = "Call Of The Haunted",
                CardLinkArrow = new List<CardLinkArrow>
                {
                    new CardLinkArrow{ LinkArrowId = 4324, LinkArrow = new LinkArrow{ Name = "Left"}},
                    new CardLinkArrow{ LinkArrowId = 4324, LinkArrow = new LinkArrow{ Name = "Right"}}
                }
            };

            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, card);

            // Assert
            result.Link.Should().Be(2);
        }

        [Test]
        public void Given_A_Card_When_Mapped_To_CardDto_Then_LinkArrows_Should_Have_A_Count_Of_2()
        {
            // Arrange
            var card = new Card
            {
                Name = "Call Of The Haunted",
                CardLinkArrow = new List<CardLinkArrow>
                {
                    new CardLinkArrow{ LinkArrowId = 4324, LinkArrow = new LinkArrow{ Name = "Left"}},
                    new CardLinkArrow{ LinkArrowId = 4324, LinkArrow = new LinkArrow{ Name = "Right"}}
                }
            };

            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, card);

            // Assert
            result.LinkArrows.Count.Should().Be(2);
        }


        [Test]
        public void Given_A_Card_When_Mapped_To_CardDto_Then_CardSubCategory_Should_Have_A_Count_Of_2()
        {
            // Arrange
            var card = new Card
            {
                Name = "Call Of The Haunted",
                CardSubCategory = new List<CardSubCategory>
                {
                    new CardSubCategory
                    {
                        SubCategoryId = 34224,
                        SubCategory = new SubCategory
                        {
                            CategoryId = 1,
                            Name = "Effect"
                        }
                        
                    },
                    new CardSubCategory
                    {
                        SubCategoryId = 4324,
                        SubCategory = new SubCategory
                        {
                            CategoryId = 1,
                            Name = "Fusion"
                        }
                        
                    }
                }
            };

            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, card);

            // Assert
            result.SubCategories.Count.Should().Be(2);
        }

        [Test]
        public void Given_A_Card_When_Mapped_To_CardDto_Then_CardSubCategory_Should_Contain_All_SubCategory_Ids()
        {
            // Arrange
            var expected = new[] {34224, 4324 };

            var card = new Card
            {
                Name = "Call Of The Haunted",
                CardSubCategory = new List<CardSubCategory>
                {
                    new CardSubCategory
                    {
                        SubCategoryId = 34224,
                        SubCategory = new SubCategory
                        {
                            CategoryId = 1,
                            Name = "Effect"
                        }
                        
                    },
                    new CardSubCategory
                    {
                        SubCategoryId = 4324,
                        SubCategory = new SubCategory
                        {
                            CategoryId = 1,
                            Name = "Fusion"
                        }
                        
                    }
                }
            };

            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, card);

            // Assert
            result.SubCategories.Select(sc => sc.Id).Should().ContainInOrder(expected);
        }


        [Test]
        public void Given_A_Card_When_Mapped_To_CardDto_Then_CardType_Should_Have_A_Count_Of_2()
        {
            // Arrange
            var card = new Card
            {
                Name = "Call Of The Haunted",
                CardType = new List<CardType>
                {
                    new CardType
                    {
                        TypeId = 4324,
                        Type = new Type
                        {
                            Name = "SpellCaster"
                        }
                    }, 
                    new CardType
                    {
                        TypeId = 532453,
                        Type = new Type
                        {
                            Name = "Machine"
                        }
                    }
                }
            };

            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, card);

            // Assert
            result.Types.Count.Should().Be(2);
        }

        [Test]
        public void Given_A_Card_When_Mapped_To_CardDto_Then_CardType_Should_Contain_All_CardType_Ids()
        {
            // Arrange
            var card = new Card
            {
                Name = "Call Of The Haunted",
                CardType = new List<CardType>
                {
                    new CardType
                    {
                        TypeId = 4324,
                        Type = new Type
                        {
                            Name = "SpellCaster"
                        }
                    },
                    new CardType
                    {
                        TypeId = 532453,
                        Type = new Type
                        {
                            Name = "Machine"
                        }
                    }
                }
            };

            // Act
            var result = CommandMapperHelper.MapToCardDto(_mapper, card);

            // Assert
            result.Types.Count.Should().Be(2);
        }

    }
}