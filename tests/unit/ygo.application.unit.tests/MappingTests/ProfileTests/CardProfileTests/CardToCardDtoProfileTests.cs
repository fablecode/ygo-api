using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using ygo.application.Configuration;
using ygo.application.Dto;
using ygo.application.Mappings.Profiles;
using ygo.application.Mappings.Resolvers;
using ygo.core.Models.Db;
using ygo.tests.core;
using Type = System.Type;

namespace ygo.application.unit.tests.MappingTests.ProfileTests.CardProfileTests
{
    [TestFixture]
    [Category(TestType.Unit)]
    public class CardToCardDtoProfileTests
    {
        private IMapper _sut;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.AddProfile<CardProfile>();
                    cfg.AddProfile<AttributeProfile>();
                    cfg.ConstructServicesUsing(Resolve);
                }
            );

            _sut = config.CreateMapper();
        }

        [Test]
        public void Given_A_Card_If_CardAttribute_Is_Null_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted"
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Attribute.Should().BeNull();
        }

        [Test]
        public void Given_A_Card_If_CardAttribute_Is_Empty_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardAttribute = new List<CardAttribute>()
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Attribute.Should().BeNull();
        }

        [Test]
        public void Given_A_Card_If_CardAttribute_Is_Valid_Should_Map_Successful()
        {
            // Arrange
            var expected = new AttributeDto
            {
                Id = 43242,
                Name = "Water"
            };

            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardAttribute = new List<CardAttribute>
                {
                    new CardAttribute
                    {
                        Attribute = new Attribute
                        {
                            Id = 43242,
                            Name = "Water"
                        }
                    }

                }
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Attribute.Should().BeEquivalentTo(expected);
        }


        [Test]
        public void Given_A_Card_If_Link_Is_Null_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted"
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Link.Should().BeNull();
        }

        [Test]
        public void Given_A_Card_If_Link_Is_Empty_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardLinkArrow = new List<CardLinkArrow>()
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Link.Should().BeNull();
        }

        [Test]
        public void Given_A_Card_If_Link_Is_Valid_Should_Map_Successful()
        {
            // Arrange
            const int expected = 1;

            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardLinkArrow = new List<CardLinkArrow>
                {
                    new CardLinkArrow
                    {
                        LinkArrow = new LinkArrow
                        {
                            Id = 43242,
                            Name = "Down"
                        }
                    }

                }
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Link.Should().Be(expected);
        }


        [Test]
        public void Given_A_Card_If_CardSubCategory_Is_Null_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted"
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.SubCategories.Should().BeNullOrEmpty();
        }

        [Test]
        public void Given_A_Card_If_CardSubCategory_Is_Empty_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardSubCategory = new List<CardSubCategory>()
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.SubCategories.Should().BeNullOrEmpty();
        }

        [Test]
        public void Given_A_Card_If_CardSubCategory_Is_Valid_Should_Map_Successful()
        {
            // Arrange
            const int expected = 1;

            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardSubCategory = new List<CardSubCategory>
                {
                    new CardSubCategory
                    {
                        SubCategory = new SubCategory
                        {
                            Id = 43242,
                            Name = "Effect",
                            CategoryId = 1
                        }
                    }

                }
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.SubCategories.Should().HaveCount(expected);
        }


        [Test]
        public void Given_A_Card_If_CardType_Is_Null_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted"
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Types.Should().BeNullOrEmpty();
        }

        [Test]
        public void Given_A_Card_If_CardType_Is_Empty_Should_Return_Null()
        {
            // Arrange
            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardType = new List<CardType>()
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Types.Should().BeNullOrEmpty();
        }

        [Test]
        public void Given_A_Card_If_CardType_Is_Valid_Should_Map_Successful()
        {
            // Arrange
            const int expected = 1;

            var source = new Card
            {
                Name = "Call Of The Haunted",
                CardType = new List<CardType>
                {
                    new CardType
                    {
                         Type = new core.Models.Db.Type
                        {
                            Id = 43242,
                            Name = "Insect"
                        }
                    }

                }
            };

            // Act
            var result = _sut.Map<CardDto>(source);

            // Assert
            result.Types.Should().HaveCount(expected);
        }


        public object Resolve(Type type)
        {
            if (type == typeof(CardImageEndpointResolver))
            {
                var options = Substitute.For<IOptions<ApplicationSettings>>();
                options.Value.Returns(new ApplicationSettings{CardImageEndpoint = "http:localhost/api/images/cards"});

                return new CardImageEndpointResolver(options);
            }

            Assert.Fail("Can not resolve type " + type.AssemblyQualifiedName);
            return null;
        }
    }
}